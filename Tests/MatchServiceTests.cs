using Xunit;
using Moq;
using Business.Services.Matches;
using DAL.Models;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class MatchServiceTests
    {
        [Fact]
        public void CreateGameTest()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var playerIdA = 1;
            var playerIdB = 2;
            var cancellationToken = new CancellationToken();

            // Act
            var result = mockMatchService.Object.CreateGame(playerIdA, playerIdB, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ResolveGameTest()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var matchId = 1;
            var cancellationToken = new CancellationToken();

            // Act
            var result = mockMatchService.Object.ResolveGame(matchId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetTest()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var matchId = 1;
            var cancellationToken = new CancellationToken();

            // Act
            var result = mockMatchService.Object.Get(matchId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var mockMatchService = new Mock<IMatchService>();
            var cancellationToken = new CancellationToken();

            // Act
            var result = mockMatchService.Object.GetAll(cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<MatchDto>>(result.Result);
        }
    }
}
