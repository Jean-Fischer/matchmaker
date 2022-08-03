using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using Business.Services.Matches;
using Business.Services.MatchMaking;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.MatchQueue;

public class MatchQueueService :IMatchQueueService
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly IMatchMakingResolver _matchMakingResolver;
    private readonly IMatchService _matchService;
    public MatchQueueService(IDbContextFactory<MatchMakingContext> dbContextFactory, IMapper mapper, IMatchMakingResolver matchMakingResolver, IMatchService matchService)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _matchMakingResolver = matchMakingResolver;
        _matchService = matchService;
    }

    public async Task<MatchQueueDto> AddToQueue(int playerId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var player = await context.Players.FirstOrDefaultAsync(s => s.Id == playerId);
        if (player == null) throw new InvalidDataException($"No player found for id {playerId}");

        if (context.MatchQueues.Any(s => s.PlayerId == playerId))
            throw new ArgumentException($"Player is already in queue");

        var matchqueue = new DAL.Models.MatchQueue() {JoinDate = DateTime.Now,PlayerId = playerId};
        var entry = await context.MatchQueues.AddAsync(matchqueue);
        await context.SaveChangesAsync();
        return _mapper.Map<MatchQueueDto>(entry.Entity);

    }

    public async Task<IEnumerable<MatchQueueDto>> GetAll()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return context.MatchQueues.ProjectTo<MatchQueueDto>(_mapper.ConfigurationProvider).ToList();
    }
    
    public async Task ProcessQueue()
    {

        var matchQueues = await GetAll();
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var matchings = await _matchMakingResolver.ResolveMatchings(matchQueues);
        foreach (var match in matchings)
        {
            await _matchService.CreateGame(match.Player1.Player.Id, match.Player2.Player.Id);
            var toDelete = await context.MatchQueues.Where(s => s.Id ==match.Player1.Id || s.Id == match.Player2.Id ).ToListAsync();
            context.MatchQueues.RemoveRange(toDelete);
        }

        await context.SaveChangesAsync();

    }
}