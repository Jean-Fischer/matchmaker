using Business.Dto;
using Business.Services.Matches;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.MatchMaking;


public class TrivialMatchMakingResolver : IMatchMakingResolver
{
    public Task<IEnumerable<Matching>> ResolveMatchings(IEnumerable<MatchQueueDto> matchQueueDtos, CancellationToken cancellationToken)
    {
        return  Task.FromResult(matchQueueDtos.Chunk(2).Where(s=>s.Length == 2).Select(s=>new Matching(){Player1 = s[0],Player2 = s[1]}));
    }
}


