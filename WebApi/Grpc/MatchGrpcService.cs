using AutoMapper;
using Business.Dto;
using Business.Services.Matches;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace WebApi.Grpc;

public class MatchGrpcService : MatchGprcService.MatchGprcServiceBase
{

    private readonly IMatchService _matchService;
    private readonly IMapper _mapper;

    public MatchGrpcService(IMatchService matchService, IMapper mapper)
    {
        _matchService = matchService;
        _mapper = mapper;
    }

    

    public override async Task<Response> GetAll(Request request, ServerCallContext context)
    {
        var res =await   _matchService.GetAll(99999, 0);
        var mappedRes = _mapper.Map<IEnumerable<MatchGrpcDto>>(res);
        var response =  new Response() ;
        response.Result.AddRange(mappedRes);
        return response;
    }

    public override async Task GetAllStream(Request request, IServerStreamWriter<MatchGrpcDto> responseStream, ServerCallContext context)
    {
        var res = await _matchService.GetAll(99999, 0);
        
        foreach (var message in res)
        {
            await responseStream.WriteAsync(_mapper.Map<MatchGrpcDto>(message));
        }
    }

    public override async Task GetAllRequested(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
    {
        
        await foreach(var item in requestStream.ReadAllAsync())
        {
            var res =await   _matchService.GetAll(99999, 0);
            var mappedRes = _mapper.Map<IEnumerable<MatchGrpcDto>>(res);
            var response =  new Response() ;
            response.Result.AddRange(mappedRes);
            await responseStream.WriteAsync(response);
        }
    }
}


public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<MatchDto,MatchGrpcDto>().ReverseMap();
        CreateMap<ParticipationDto,ParticipationGrpcDto>().ReverseMap();
        CreateMap<PlayerDto,PlayerGrpcDto>().ReverseMap();
        CreateMap<DateTime, Timestamp>().ConvertUsing(time =>  Timestamp.FromDateTime(time.ToUniversalTime()));
    }
}
