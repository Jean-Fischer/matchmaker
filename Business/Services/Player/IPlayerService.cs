using Business.Dto;

namespace Business.Services.Player;

public interface IPlayerService
{
    Task<PlayerDto> CreatePlayer(PlayerDto player);
    Task<IEnumerable<PlayerDto>> GetAll();
    Task<IEnumerable<PlayerDto>> GetUnlisted();
}