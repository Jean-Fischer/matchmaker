using DAL.Models;

namespace Business.Helpers;

public static class MatchHelper
{
    public static (Participation First, Participation Second) GetParticipations(this Match match)
    {
        if (match.Participations.Count != 2)
            throw new NotImplementedException("SimulateMatchResult implemented only for 2 players");
        return (match.Participations[0], match.Participations[1]);
    }
}