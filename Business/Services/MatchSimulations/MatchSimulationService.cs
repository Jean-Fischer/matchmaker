using DAL.Models;

namespace Business.Services.MatchSimulations;

public class MatchSimulationService : IMatchSimulationService
{
    public Match SimulateMatchResult(Match match)
    {
        
        var random = new Random().NextDouble() > 0.5;
        var participations = match.GetParticipations();
        participations.A.HasWon = random;
        participations.B.HasWon = !random;
        
        return match;
    } 
}