using Business.Dto;
using Business.Services.Matches;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController
{
    private readonly IMatchService _matchService;


    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpPost("create")]
    public async Task<int> CreateMatch(int playerIdA, int playerIdB, CancellationToken cancellationToken)
    {
        return await _matchService.CreateGame(playerIdA, playerIdB, cancellationToken);
    }

    [HttpPost("resolve/{matchid}")]
    public async Task ResolveGame(int matchid, CancellationToken cancellationToken)
    {
        await _matchService.ResolveGame(matchid, cancellationToken);
    }

    [HttpGet("{matchid}")]
    public async Task<MatchDto> Get(int matchid, CancellationToken cancellationToken)
    {
        return await _matchService.Get(matchid, cancellationToken);
    }

    [HttpGet("")]
    public async Task<IEnumerable<MatchDto>> Get(CancellationToken cancellationToken, [FromQuery] int pageSize = 999999,
        [FromQuery] int pageNumber = 0)
    {
        return await _matchService.GetAll(cancellationToken, pageSize, pageNumber);
    }
}