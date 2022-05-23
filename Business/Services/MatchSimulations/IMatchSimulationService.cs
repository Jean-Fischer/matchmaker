using DAL.Models;

namespace Business.Services.MatchSimulations;

public interface IMatchSimulationService
{
    Match SimulateMatchResult(Match match);
}