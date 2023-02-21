using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using Business.Services.RandomUserGeneration;
using Business.Services.RandomUserGeneration.Model;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Business.Services.PlayerService;

public class PlayerService : IPlayerService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IRandomUserGeneration _randomUserGeneration;

    public PlayerService(IMapper mapper, IDbContextFactory<MatchMakingContext> dbContextFactory, IRandomUserGeneration randomUserGeneration)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
        _randomUserGeneration = randomUserGeneration;
    }

    public async Task<PlayerDto> CreatePlayer(PlayerDto playerDto, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<Player>(playerDto);
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var newEntry = context.Players.Add(player);
        await context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PlayerDto>(newEntry.Entity);
    }

    public async Task GenerateRandomPlayers(int playerNumbers, CancellationToken cancellationToken)
    {
        var players = await GeneratePlayers(playerNumbers,cancellationToken);
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await context.Players.AddRangeAsync(players);
        await context.SaveChangesAsync(cancellationToken);
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


    private async Task<IEnumerable<Player>> GeneratePlayers(int numberOfPlayers, CancellationToken cancellationToken)
    {

        var result = await _randomUserGeneration.GenerateRandomUsers(numberOfPlayers, cancellationToken);
        var players = result.Results.Select(s => new Player() { Nickname = $"{s.Name.First} {s.Name.Last}", Rank = 1200 });

        return players;
    }

    

}



