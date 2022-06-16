using Business.Dto;
using DAL.Migrations;

namespace Business.Services.MatchQueue;

public interface IMatchQueueService
{
    Task<MatchQueueDto> AddToQueue(int playerId);

    Task<IEnumerable<MatchQueueDto>> GetAll();

}