using Business.Dto;
using Business.Services.Player;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PlayerController
{
    private readonly IDbContextFactory<MatchMakingContext> _dbContextFactory;
    private readonly IPlayerService _playerService;
    
    public PlayerController(IDbContextFactory<MatchMakingContext> dbContextFactory, IPlayerService playerService)
    {
        _dbContextFactory = dbContextFactory;
        _playerService = playerService;
    }
    
    
    [HttpGet("")]
    public async Task<IEnumerable<Player>> GetPlayers()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return context.Players.ToList();
    }

    [HttpPost("")]
    public async Task<PlayerDto> CreatePlayer(PlayerDto player)
    {
        return await _playerService.CreatePlayer(player);
    } 
    
    
    
}