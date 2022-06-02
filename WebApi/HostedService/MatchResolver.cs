using System.Security.Cryptography.X509Certificates;
using Business.Services.Matches;

namespace WebApi.HostedService;

public class MatchResolver : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    public MatchResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
            }
            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

}