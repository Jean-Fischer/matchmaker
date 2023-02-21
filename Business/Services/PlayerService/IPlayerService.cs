using Business.Dto;

namespace Business.Services.PlayerService;

public interface IPlayerService
{
    Task<PlayerDto> CreatePlayer(PlayerDto playerDto, CancellationToken cancellationToken);
    Task<IEnumerable<PlayerDto>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<PlayerDto>> GetUnlisted(CancellationToken cancellationToken);

    Task GenerateRandomPlayers(int playerNumbers, CancellationToken cancellationToken);
}