using Business.Services.Matches;

namespace WebApi.HostedService;

public class MatchResolver : IHostedService
{

    private readonly IServiceProvider _serviceProvider;
    public MatchResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public  Task StartAsync(CancellationToken cancellationToken)
    {
        
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IMatchService matchService =
                scope.ServiceProvider.GetRequiredService<IMatchService>();

            return matchService.ResolveAllUnresolvedMatches();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}