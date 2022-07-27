using Business.Services.Matches;
using Microsoft.AspNetCore.SignalR;
using WebApi.SignalR;

namespace WebApi.HostedService;

public class MatchResolver : BackgroundService
{
    private readonly IHubContext<MatchHub> _hubContext;

    private readonly IServiceProvider _serviceProvider;

    public MatchResolver(IServiceProvider serviceProvider, IHubContext<MatchHub> hubContext)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(new TimeSpan(0, 0, 20));
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var matchService =
                    scope.ServiceProvider.GetRequiredService<IMatchService>();

                await matchService.ResolveAllUnresolvedMatches(stoppingToken);
                await _hubContext.Clients.All.SendAsync("RefreshMatchList",
                    await matchService.GetAll(stoppingToken), stoppingToken);
            }

            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }
}