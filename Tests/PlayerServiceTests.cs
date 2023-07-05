using AutoMapper;
using Business;
using Business.Dto;
using Business.Services.PlayerService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests
{
    public class PlayerServiceTests
    {
        private readonly Mock<IDbContextFactory<MatchMakingContext>> _dbContextFactory = new();

        private readonly IMapper _mapper =
            new Mapper(new MapperConfiguration(config => config.AddProfile(new BusinessMappingProfile())));

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


        [Fact]
        private async Task GivenAValidPlayerItShouldSaveIt()
        {
            var test = await _sut.CreatePlayer(new PlayerDto { Nickname = "Toto", Rank = 1200 }, new CancellationToken());
            Assert.Equal("Toto", test.Nickname);
            Assert.Equal(1200, test.Rank);
        }

        [Fact]
        private async Task GivenValidNumberOfPlayersItShouldGenerateThem()
        {
            var numberOfPlayers = 5;
            await _sut.GenerateRandomPlayers(numberOfPlayers, new CancellationToken());

            using var context = _dbContextFactory.Object.CreateDbContext();
            var playersInDb = await context.Players.CountAsync();

            Assert.Equal(numberOfPlayers, playersInDb);
        }

        [Fact]
        private async Task GetAllShouldReturnAllPlayers()
        {
            var players = await _sut.GetAll(new CancellationToken());

            using var context = _dbContextFactory.Object.CreateDbContext();
            var playersInDb = await context.Players.CountAsync();

            Assert.Equal(playersInDb, players.Count());
        }

        [Fact]
        private async Task GetUnlistedShouldReturnOnlyUnlistedPlayers()
        {
            var unlistedPlayers = await _sut.GetUnlisted(new CancellationToken());

            using var context = _dbContextFactory.Object.CreateDbContext();
            var unlistedPlayersInDb = await context.Players.Include(s => s.MatchQueues).Where(s => !s.MatchQueues.Any()).CountAsync();

            Assert.Equal(unlistedPlayersInDb, unlistedPlayers.Count());
        }
    }
}

