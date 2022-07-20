using Business.Dto;

namespace Business.Services.Player;

public interface IPlayerService
{
    Task<PlayerDto> CreatePlayer(PlayerDto player, CancellationToken cancellationToken);
    Task<IEnumerable<PlayerDto>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<PlayerDto>> GetUnlisted(CancellationToken cancellationToken);
}