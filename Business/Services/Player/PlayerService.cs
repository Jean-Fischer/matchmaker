using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Player;

public class PlayerService : IPlayerService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;

    public PlayerService(IMapper mapper, IDbContextFactory<MatchMakingContext> dbContextFactory)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<PlayerDto> CreatePlayer(PlayerDto playerDto, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<DAL.Models.Player>(playerDto);
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);   
        var newEntry = context.Players.Add(player);
        await context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PlayerDto>(newEntry.Entity);
    }

    public async Task<IEnumerable<PlayerDto>> GetAll(CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Players.ProjectTo<PlayerDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<PlayerDto>> GetUnlisted(CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Players.Include(s => s.MatchQueues).Where(s => !s.MatchQueues.Any()).ProjectTo<PlayerDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
    }
}