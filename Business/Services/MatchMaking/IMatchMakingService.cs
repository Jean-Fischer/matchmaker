using Business.Dto;

namespace Business.Services.MatchMaking;

public interface IMatchMakingService
{
    Task ResolveQueue(IEnumerable<MatchQueueDto> matchQueues);
}