using Business.Dto;
using Business.Services.PlayerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
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


    [HttpPut("generateplayers/{numberOfPlayers}")]
    public async Task CreatePlayer(int numberOfPlayers, CancellationToken cancellationToken)
    {
        await _playerService.GenerateRandomPlayers(numberOfPlayers, cancellationToken);
    }
}