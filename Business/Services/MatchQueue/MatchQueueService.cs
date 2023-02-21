using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using Business.Services.Matches;
using Business.Services.MatchMaking;
using Business.Services.PlayerService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.MatchQueue;

public class MatchQueueService :IMatchQueueService
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IMatchMakingResolver _matchMakingResolver;
    private readonly IMatchService _matchService;
    private readonly IPlayerService _playerService;

    public MatchQueueService(IDbContextFactory<MatchMakingContext> dbContextFactory, IMapper mapper, IMatchMakingResolver matchMakingResolver, IMatchService matchService, IPlayerService playerService)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _matchMakingResolver = matchMakingResolver;
        _matchService = matchService;
        _playerService = playerService;
    }

    public async Task<MatchQueueDto> AddToQueue(int playerId, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var player = await context.Players.FirstOrDefaultAsync(s => s.Id == playerId, cancellationToken: cancellationToken);
        if (player == null) throw new InvalidDataException($"No player found for id {playerId}");

        if (context.MatchQueues.Any(s => s.PlayerId == playerId))
            throw new ArgumentException($"Player is already in queue");

        var matchqueue = new DAL.Models.MatchQueue() {JoinDate = DateTime.Now,PlayerId = playerId};
        var entry = await context.MatchQueues.AddAsync(matchqueue, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<MatchQueueDto>(entry.Entity);

    }

    public async Task AddToQueueInBulk(List<int> playerIds, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var players = context.Players.Where(s => playerIds.Contains(s.Id) && !s.MatchQueues.Any());
        
        foreach(var player in players)
        {
            player.MatchQueues.Add(new DAL.Models.MatchQueue() { JoinDate = DateTime.Now });
        }

        await context.SaveChangesAsync(cancellationToken);

    }


    public async Task<IEnumerable<MatchQueueDto>> GetAll(CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return context.MatchQueues.ProjectTo<MatchQueueDto>(_mapper.ConfigurationProvider).ToList();
    }
    
    public async Task ProcessQueue(CancellationToken cancellationToken)
    {

        var matchQueues = await GetAll(cancellationToken);
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var matchings = await _matchMakingResolver.ResolveMatchings(matchQueues,cancellationToken);
        foreach (var match in matchings)
        {
            await _matchService.CreateGame(match.Player1.Player.Id, match.Player2.Player.Id, cancellationToken);
            var toDelete = await context.MatchQueues.Where(s => s.Id ==match.Player1.Id || s.Id == match.Player2.Id ).ToListAsync(cancellationToken: cancellationToken);
            context.MatchQueues.RemoveRange(toDelete);
        }

        await context.SaveChangesAsync(cancellationToken);

    }

    public async Task QueueRandomPlayers(float percentageOfPlayerToQueue, CancellationToken cancellationToken)
    {
        var players = await _playerService.GetUnlisted(cancellationToken);
        var numberOfPlayerToSelect = (int)(players.Count() * percentageOfPlayerToQueue);
        var rnd = new Random();
        var selectedPlayers = players.OrderBy(x => rnd.Next()).Take(numberOfPlayerToSelect);
        await AddToQueueInBulk(selectedPlayers.Select(s => s.Id).ToList(), cancellationToken);

    }

}