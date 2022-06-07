using Business.Services.Enum;

namespace Business.Services.Rating;

public interface IRatingService
{
    double GetNewRating(double oldRating, double opponentRating, GameResultCoefficient resultCoef);
    double GetExpectedOutcome(double consideredRating, double opponentRating);



}