using AutoMapper;
using Business.Dto;
using Business.Services.Matches;
using Business.Technical;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NetMQ;

namespace WebApi.Grpc;

public class MatchGrpcService : MatchGprcService.MatchGprcServiceBase
{
    private readonly IMapper _mapper;
    private readonly IMatchService _matchService;
    private readonly SocketService _socketService;

    public MatchGrpcService(IMatchService matchService, IMapper mapper, SocketService socketService)
    {
        _matchService = matchService;
        _mapper = mapper;
        _socketService = socketService;
    }


    public override async Task<Response> GetAll(Request request, ServerCallContext context)
    {
        var res = await _matchService.GetAll(context.CancellationToken, 99999, 0);
        var mappedRes = _mapper.Map<IEnumerable<MatchGrpcDto>>(res);
        var response = new Response();
        response.Result.AddRange(mappedRes);
        return response;
    }

    public override async Task GetAllStream(Request request, IServerStreamWriter<MatchGrpcDto> responseStream,
        ServerCallContext context)
    {
        var res = await _matchService.GetAll(context.CancellationToken, 99999, 0);
        while (!context.CancellationToken.IsCancellationRequested)
            //timer every 10 seconds
            foreach (var message in res)
                await responseStream.WriteAsync(_mapper.Map<MatchGrpcDto>(message));
    }

    public override async Task GetAllRefreshed(Request request, IServerStreamWriter<Response> responseStream,
        ServerCallContext context)
    {
        using var subSocket = _socketService.GetSubscriberSocket();
        subSocket.Options.ReceiveHighWatermark = 1000;

        subSocket.Subscribe("RefreshMatches");

        var firstLoop = true; //dont wait for refresh signal on the first loop
        while (!context.CancellationToken.IsCancellationRequested)
            try
            {
                if (!firstLoop)
                {
                    subSocket.ReceiveFrameString();
                    subSocket.ReceiveFrameString();
                }
                else
                {
                    firstLoop = !firstLoop;
                }

                var res = await _matchService.GetAll(context.CancellationToken, 99999, 0);
                var mappedRes = _mapper.Map<IEnumerable<MatchGrpcDto>>(res);
                var response = new Response();
                response.Result.AddRange(mappedRes);
                await responseStream.WriteAsync(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
    }
}

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<MatchDto, MatchGrpcDto>().ReverseMap();
        CreateMap<ParticipationDto, ParticipationGrpcDto>().ReverseMap();
        CreateMap<PlayerDto, PlayerGrpcDto>().ReverseMap();
        CreateMap<DateTime, Timestamp>().ConvertUsing(time => Timestamp.FromDateTime(time.ToUniversalTime()));
    }
}