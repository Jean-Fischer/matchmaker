using System.Globalization;
using Business.Services.Enum;
using Business.Services.Rating;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class RatingTests
{
    private readonly ITestOutputHelper _log;

    private readonly RatingService _sut;

    public RatingTests(ITestOutputHelper log)
    {
        _log = log;
        _sut = new RatingService();
    }


    [Theory]
    [InlineData(0, 50)]
    [InlineData(1500, 20)]
    [InlineData(1200, 1520)]
    [InlineData(double.MinValue, double.MaxValue)]
    [InlineData(double.MaxValue, double.MinValue)]
    public async Task GetExpectedProbabilityShouldBeSuperiorIfRatingIsSupererior(double consideredRating,
        double opponentRating)
    {
        var expectedOutcome = _sut.GetExpectedOutcome(consideredRating, opponentRating);
        _log.WriteLine(expectedOutcome.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(expectedOutcome > 0.5, consideredRating > opponentRating);
    }

    [Theory]
    [InlineData(0, 50)]
    [InlineData(double.MinValue, double.MaxValue)]
    [InlineData(double.MaxValue, double.MinValue)]
    [InlineData(50, 0)]
    [InlineData(50, 50)]
    public async Task NewRatingShouldIncreaseOrStayTheSameOnWin(double oldRating, double opponentRating)
    {
        var newRating = _sut.GetNewRating(oldRating, opponentRating, GameResultCoefficient.Win);
        _log.WriteLine(newRating.ToString());
        Assert.True(newRating >= oldRating);
    }

    [Theory]
    [InlineData(0, 50)]
    [InlineData(double.MinValue, double.MaxValue)]
    [InlineData(double.MaxValue, double.MinValue)]
    [InlineData(50, 0)]
    [InlineData(50, 50)]
    public async Task NewRatingShouldDecreaseOrStayTheSameOnLose(double oldRating, double opponentRating)
    {
        var newRating = _sut.GetNewRating(oldRating, opponentRating, GameResultCoefficient.Lose);
        _log.WriteLine(newRating.ToString());
        Assert.True(newRating <= oldRating);
    }
}