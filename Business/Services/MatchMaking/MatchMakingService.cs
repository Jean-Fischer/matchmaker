using Business.Dto;

namespace Business.Services.MatchMaking;

public class MatchMakingService : IMatchMakingService
{
    public async Task ResolveQueue(IEnumerable<MatchQueueDto> matchQueues)
    {
        var now = DateTime.Now;
    }

    public async Task<double> ComputeDistance(MatchQueueDto matchQueue1, MatchQueueDto matchQueue2)
    {
        return 0;
    }


}