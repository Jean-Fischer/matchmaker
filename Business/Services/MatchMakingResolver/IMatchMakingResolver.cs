using Business.Dto;

namespace Business.Services.MatchMaking;

public interface IMatchMakingResolver
{
    Task<IEnumerable<Matching>> ResolveMatchings(IEnumerable<MatchQueueDto> matchQueueDtos);
}

public class Matching
{
    public MatchQueueDto Player1 { get; set; }
    public MatchQueueDto Player2 { get; set; }

}