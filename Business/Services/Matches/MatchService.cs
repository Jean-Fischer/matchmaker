using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using Business.Services.Enum;
using Business.Services.MatchSimulations;
using Business.Services.Rating;
using Business.Technical;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NetMQ;
using NetMQ.Sockets;

namespace Business.Services.Matches;

public class MatchService : IMatchService
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IMatchSimulationService _matchSimulationService;
    private readonly IRatingService _ratingService;
    private readonly MatchMakingContext _context;
    private readonly IMapper _mapper;
    private readonly SocketService _socketService;

    public MatchService(IDbContextFactory<MatchMakingContext> dbContextFactory,
        IMatchSimulationService matchSimulationService, IRatingService ratingService, MatchMakingContext context,
        IMapper mapper, SocketService socketService)
    {
        _dbContextFactory = dbContextFactory;
        _matchSimulationService = matchSimulationService;
        _ratingService = ratingService;
        _context = context;
        _mapper = mapper;
        _socketService = socketService;
    }

    public async Task<int> CreateGame(int playerIdA, int playerIdB, CancellationToken cancellationToken)
    {
        var participationA = await CreateParticipation(playerIdA, cancellationToken);
        var participationB = await CreateParticipation(playerIdB, cancellationToken);

        var match = new Match()
        {
            Participations = new List<Participation> { participationA, participationB },
            RegistrationDate = DateTime.Now
        };
        _context.Matches.Add(match);
        await _context.SaveChangesAsync(cancellationToken);
        return match.Id;
    }

    private async Task<Participation> CreateParticipation(int playerId, CancellationToken cancellationToken)
    {
        var player =
            await _context.Players.SingleOrDefaultAsync(s => s.Id == playerId, cancellationToken: cancellationToken);
        if (player == null) throw new InvalidDataException($"No player found for id {playerId}");
        var participation = new Participation()
        {
            Player = player,
            StartingRank = player.Rank
        };
        return participation;
    }

    public async Task<Match> ResolveGame(int matchId, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var match = context.Matches
            .Include(s => s.Participations)
            .ThenInclude(s => s.Player)
            .FirstOrDefault(s => s.Id == matchId);
        _matchSimulationService.SimulateMatchResult(match);
        var participations = match.GetParticipations();
        var newPlayerARating = (int)_ratingService.GetNewRating(participations.A.Player.Rank,
            participations.B.Player.Rank,
            GameResultCoefficientHelper.GetCoefficientFromResult(participations.A.HasWon));
        var newPlayerBRating = (int)_ratingService.GetNewRating(participations.B.Player.Rank,
            participations.A.Player.Rank,
            GameResultCoefficientHelper.GetCoefficientFromResult(participations.B.HasWon));
        match.PlayDate = DateTime.Now;
        participations.A.FinishingRank = newPlayerARating;
        participations.A.RankDifference = newPlayerARating - participations.A.StartingRank;
        participations.A.Player.Rank = newPlayerARating;
        participations.B.FinishingRank = newPlayerBRating;
        participations.B.RankDifference = newPlayerBRating - participations.B.StartingRank;
        participations.B.Player.Rank = newPlayerBRating;

        await context.SaveChangesAsync();
        return match;
    }

    public async Task<MatchDto> Get(int matchId, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var match = context.Matches.Include(s => s.Participations)
            .ThenInclude(s => s.Player)
            .FirstOrDefault(s => s.Id == matchId);
        return _mapper.Map<MatchDto>(match);
    }

    public async Task<IEnumerable<MatchDto>> GetAll(CancellationToken cancellationToken, int pageSize = 100000,
        int pageNumber = 0)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Matches.Include(s => s.Participations)
            .ThenInclude(s => s.Player)
            .AsSplitQuery()
            .OrderByDescending(s => s.RegistrationDate)
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ProjectTo<MatchDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task ResolveAllUnresolvedMatches(CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var unresolvedMatches = context.Matches.Where(s => s.PlayDate == null).ToList();
        foreach (var match in unresolvedMatches)
        {
            await ResolveGame(match.Id, cancellationToken);
        }


        var pubSocket = _socketService.PublisherSocket;


        pubSocket.SendMoreFrame("RefreshMatches").SendFrame("test");
    }
}