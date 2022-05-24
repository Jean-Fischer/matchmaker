using Business.Dto;

namespace Business.Services.Player;

public interface IPlayerService
{
    Task<PlayerDto> CreatePlayer(PlayerDto player);
}