using Business.Services.Matches;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.SignalR;

public class MatchHub : Hub
{
    private readonly IHttpContextAccessor _context;
    private readonly IMatchService _matchService;

    public MatchHub(IMatchService matchService, IHttpContextAccessor context)
    {
        _matchService = matchService;
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("RefreshMatchList",
            await _matchService.GetAll(new CancellationToken(), 99999, 0));
    }

    public async Task RefreshMatchList()
    {
        await Clients.All.SendAsync("RefreshMatchList",
            await _matchService.GetAll(new CancellationToken(), 99999, 0));
    }
}