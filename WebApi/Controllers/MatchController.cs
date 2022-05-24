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
    public async Task<int> CreateMatch(int playerIdA, int playerIdB)
    {
        return await _matchService.CreateGame(playerIdA, playerIdB);
    }

    [HttpPost("resolve/{matchid}")]
    public async Task ResolveGame(int matchid)
    {
        await _matchService.ResolveGame(matchid);
    }
    
    [HttpGet("{matchid}")]
    public async Task<MatchDto> Get(int matchid)
    {
        return await _matchService.Get(matchid);
    }
    
    [HttpGet("")]
    public async Task<IEnumerable<MatchDto>> Get([FromQuery]int pageSize = 999999, [FromQuery]int pageNumber = 0)
    {
        return await _matchService.GetAll(pageSize,pageNumber);
    }
    
}