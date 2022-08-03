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
    public async Task<IEnumerable<PlayerDto>> GetPlayers()
    {
        return await _playerService.GetAll();
    }
    
    [HttpGet("unlisted")]
    public async Task<IEnumerable<PlayerDto>> GetUnlistedPlayers()
    {
        return await  _playerService.GetUnlisted();
    }

    [HttpPost("")]
    public async Task<PlayerDto> CreatePlayer(PlayerDto player)
    {
        return await _playerService.CreatePlayer(player);
    } 
    
    
    
}