using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.MatchQueue;

public class MatchQueueService :IMatchQueueService
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public MatchQueueService(IDbContextFactory<MatchMakingContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
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
}