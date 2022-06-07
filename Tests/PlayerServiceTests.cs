using AutoMapper;
using Business;
using Business.Dto;
using Business.Services.Player;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Xunit;

namespace Tests;

public class PlayerServiceTests
{
    private readonly IMapper _mapper =
        new Mapper(new MapperConfiguration(config => config.AddProfile(new BusinessMappingProfile())));

    private readonly Mock<IDbContextFactory<MatchMakingContext>> _dbContextFactory = new();

    private readonly IPlayerService _sut;


    public PlayerServiceTests()
    {
        _dbContextFactory.Setup(f => f.CreateDbContext())
            .Returns(() => new MatchMakingContext(new DbContextOptionsBuilder<MatchMakingContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .Options));
        _dbContextFactory.Setup(f => f.CreateDbContextAsync(CancellationToken.None))
            .Returns(Task.FromResult(new MatchMakingContext(new DbContextOptionsBuilder<MatchMakingContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .Options)));
        _sut = new PlayerService(_mapper, _dbContextFactory.Object);

        Seed();
    }

    private void Seed()
    {
        using var context = _dbContextFactory.Object.CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }


    [Fact()]
    private async Task GivenAValidPlayerItShouldSaveIt()
    {
        var test = await _sut.CreatePlayer(new PlayerDto() { Nickname = "Toto", Rank = 1200 });
        Assert.Equal("Toto",test.Nickname);
        Assert.Equal(1200,test.Rank);
    }

}