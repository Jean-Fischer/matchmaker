using System.Security.Cryptography.X509Certificates;
using Business.Services.Matches;
using Microsoft.AspNetCore.SignalR;
using WebApi.SignalR;

namespace WebApi.HostedService;

public class MatchResolver : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<MatchHub> _hubContext;
    
    public MatchResolver(IServiceProvider serviceProvider, IHubContext<MatchHub> hubContext)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(new TimeSpan(0,0,2));
        while (true)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMatchService matchService =
                    scope.ServiceProvider.GetRequiredService<IMatchService>();
                
                await  matchService.ResolveAllUnresolvedMatches();
                await _hubContext.Clients.All.SendAsync("RefreshMatchList",await matchService.GetAll(99999,0), cancellationToken: stoppingToken);
                //await _hubContext.Clients.All.SendCoreAsync();
            }
            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

}