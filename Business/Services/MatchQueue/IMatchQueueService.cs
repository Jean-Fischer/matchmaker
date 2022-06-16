using Business.Dto;
using Business.Services.MatchMaking;
using DAL.Migrations;

namespace Business.Services.MatchQueue;

public interface IMatchQueueService
{
    Task<MatchQueueDto> AddToQueue(int playerId);

    Task<IEnumerable<MatchQueueDto>> GetAll();
    
    Task ProcessQueue();

}