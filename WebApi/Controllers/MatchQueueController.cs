using Business.Dto;
using Business.Services.MatchQueue;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchQueueController
{
    private readonly IMatchQueueService _matchQueueService;


    public MatchQueueController(IMatchQueueService matchQueueService)
    {
        _matchQueueService = matchQueueService;
    }


    [HttpPut]
    public async Task<MatchQueueDto> AddPlayerToQueue([FromQuery] int playerId, CancellationToken cancellationToken)
    {
        return await _matchQueueService.AddToQueue(playerId, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<MatchQueueDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _matchQueueService.GetAll(cancellationToken);
    }

    [HttpPost("process")]
    public async Task ProcessQueue(CancellationToken cancellationToken)
    {
        await _matchQueueService.ProcessQueue(cancellationToken);
    }
}