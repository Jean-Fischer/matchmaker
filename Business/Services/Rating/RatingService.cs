using System.Security.Cryptography;
using Business.Services.Enum;

namespace Business.Services.Rating;

public class RatingService: IRatingService
{

    private readonly int K = 40;
    
    public double GetExpectedOutcome(double consideredRating, double opponentRating)
    {
        var D = consideredRating - opponentRating;
        return (1) / (1 + Math.Pow(10, (-D / 400)));
    }


    

    public double GetNewRating(double oldRating, GameResultCoefficient resultCoef, double expectedOutcome)
    {
        return oldRating + K * ((double)resultCoef - expectedOutcome);
    }
    
    public double GetNewRating(double oldRating, double opponentRating, GameResultCoefficient resultCoef)
    {
        var expectedOutcome = GetExpectedOutcome(oldRating, opponentRating);
        return oldRating + K * ((double)resultCoef - expectedOutcome);
    }
}