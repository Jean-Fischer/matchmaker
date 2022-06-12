
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using Business.Services.Enum;
using Business.Services.MatchSimulations;
using Business.Services.Rating;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Matches;

public class MatchService : IMatchService
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IMatchSimulationService _matchSimulationService;
    private readonly IRatingService _ratingService;
    private readonly MatchMakingContext _context;
    private readonly IMapper _mapper;

    public MatchService(IDbContextFactory<MatchMakingContext> dbContextFactory, IMatchSimulationService matchSimulationService, IRatingService ratingService, MatchMakingContext context, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _matchSimulationService = matchSimulationService;
        _ratingService = ratingService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CreateGame(int playerIdA,int playerIdB)
    {
        var participationA = await CreateParticipation(playerIdA);
        var participationB = await CreateParticipation(playerIdB);

        var match = new Match() {Participations = new List<Participation>{participationA,participationB},
            RegistrationDate = DateTime.Now};
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();
        return match.Id;
    }

    private async Task<Participation> CreateParticipation(int playerId)
    {
        var player = _context.Players.SingleOrDefault(s => s.Id == playerId);
        if (player == null) throw new InvalidDataException($"No player found for id {playerId}");
        var participation = new Participation()
        {
            Player = player,
            StartingRank = player.Rank
        };
        return participation;
    }

    public async Task<Match> ResolveGame(int matchId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var match = context.Matches
            .Include(s=>s.Participations)
            .ThenInclude(s=>s.Player)
            .FirstOrDefault(s => s.Id == matchId);
        _matchSimulationService.SimulateMatchResult(match);
        var participations = match.GetParticipations();
        var newPlayerARating = (int) _ratingService.GetNewRating(participations.A.Player.Rank,
            participations.B.Player.Rank,
            GameResultCoefficientHelper.GetCoefficientFromResult(participations.A.HasWon));
        var newPlayerBRating = (int) _ratingService.GetNewRating(participations.B.Player.Rank,
            participations.A.Player.Rank,
            GameResultCoefficientHelper.GetCoefficientFromResult(participations.B.HasWon));
        match.PlayDate = DateTime.Now;
        participations.A.FinishingRank =  newPlayerARating ;
        participations.A.RankDifference =  newPlayerARating - participations.A.StartingRank ;
        participations.A.Player.Rank =  newPlayerARating;
        participations.B.FinishingRank = newPlayerBRating ;
        participations.B.RankDifference =  newPlayerBRating - participations.B.StartingRank ;
        participations.B.Player.Rank = newPlayerBRating ;

        await context.SaveChangesAsync();
        return match;
    }

    public async Task<MatchDto> Get(int matchId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
            var match = context.Matches.Include(s=>s.Participations)
            .ThenInclude(s=>s.Player)
            .FirstOrDefault(s => s.Id == matchId);
        return _mapper.Map<MatchDto>(match);
    }

    public async Task<IEnumerable<MatchDto>> GetAll(int pageSize = 100000, int pageNumber = 0)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Matches.Include(s => s.Participations)
            .ThenInclude(s => s.Player)
            .AsSplitQuery()
            //.Skip(pageSize*pageNumber)
            //.Take(pageSize)
            .ProjectTo<MatchDto>(_mapper.ConfigurationProvider).ToListAsync();
        
    }

    public async Task ResolveAllUnresolvedMatches()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var unresolvedMatches = context.Matches.Where(s => s.PlayDate == null).ToList();
        foreach (var match in unresolvedMatches)
        {
            await ResolveGame(match.Id);
        }
    }
}