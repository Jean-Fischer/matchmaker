using Business.Dto;
using Business.Services.Player;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IEnumerable<PlayerDto>> GetPlayers(CancellationToken cancellationToken)
    {
        return await _playerService.GetAll(cancellationToken);
    }

    [HttpGet("unlisted")]
    public async Task<IEnumerable<PlayerDto>> GetUnlistedPlayers(CancellationToken cancellationToken)
    {
        return await _playerService.GetUnlisted(cancellationToken);
    }

    [HttpPost("")]
    public async Task<PlayerDto> CreatePlayer(PlayerDto player, CancellationToken cancellationToken)
    {
        return await _playerService.CreatePlayer(player, cancellationToken);
    }
}