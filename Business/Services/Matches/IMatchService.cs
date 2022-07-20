using Business.Dto;
using DAL.Models;

namespace Business.Services.Matches;

public interface IMatchService
{
    Task<int> CreateGame(int playerIdA, int playerIdB, CancellationToken cancellationToken);
    Task<Match> ResolveGame(int matchId, CancellationToken cancellationToken);
    Task<MatchDto> Get(int matchId, CancellationToken cancellationToken);
    Task<IEnumerable<MatchDto>> GetAll(CancellationToken cancellationToken, int pageSize = 100000, int pageNumber = 0);
    Task ResolveAllUnresolvedMatches(CancellationToken cancellationToken);
}