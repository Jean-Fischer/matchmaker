using Business.Dto;
using DAL.Models;

namespace Business.Services.Matches;

public interface IMatchService
{
    Task<int> CreateGame(int playerIdA, int playerIdB);
    Task<Match> ResolveGame(int matchId);
    Task<MatchDto> Get(int matchId);
    Task<IEnumerable<MatchDto>> GetAll(int pageSize, int pageNumber);
}