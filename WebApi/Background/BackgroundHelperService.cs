using Business.Services.Matches;
using Business.Services.MatchQueue;
using Business.Services.PlayerService;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using WebApi.SignalR;

namespace WebApi.Background;

public class BackgroundHelperService
{
    private readonly IHubContext<MatchHub> _hubContext;
    private readonly IMatchQueueService _matchQueueService;
    private readonly IMatchService _matchService;


    public BackgroundHelperService(IMatchService matchService, IHubContext<MatchHub> hubContext,
        IMatchQueueService matchQueueService)
    {
        _matchService = matchService;
        _hubContext = hubContext;
        _matchQueueService = matchQueueService;
    }

    [Queue("default")]
    [AutomaticRetry(Attempts = 0)]
    public async Task ProcessQueue(CancellationToken cancellationToken)
    {
        await _matchQueueService.ProcessQueue(cancellationToken);
    }

    [Queue("default")]
    [AutomaticRetry(Attempts = 0)]
    public async Task ResolveGames(CancellationToken cancellationToken)
    {
        await _matchService.ResolveAllUnresolvedMatches(cancellationToken);

        await _hubContext.Clients.All.SendAsync("RefreshMatchList",
            await _matchService.GetAll(cancellationToken), cancellationToken);
    }

    [Queue("default")]
    [AutomaticRetry(Attempts = 0)]
    public async Task QueueRandomPlayers(float percentageOfPlayerToQueue,CancellationToken cancellationToken)
    {
        await _matchQueueService.QueueRandomPlayers(percentageOfPlayerToQueue, cancellationToken);

    }
}