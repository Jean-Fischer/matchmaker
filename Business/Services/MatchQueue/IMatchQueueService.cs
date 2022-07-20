﻿using Business.Dto;
using Business.Services.MatchMaking;
using DAL.Migrations;

namespace Business.Services.MatchQueue;

public interface IMatchQueueService
{
    Task<MatchQueueDto> AddToQueue(int playerId, CancellationToken cancellationToken);

    Task<IEnumerable<MatchQueueDto>> GetAll(CancellationToken cancellationToken);
    
    Task ProcessQueue(CancellationToken cancellationToken);

}