using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class PlayerController
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;

    public PlayerController(IDbContextFactory<MatchMakingContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    
    [HttpGet("")]
    public async Task<IEnumerable<Player>> GetPlayers()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return context.Players.ToList();
    }
    
}