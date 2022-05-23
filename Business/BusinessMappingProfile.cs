using AutoMapper;
using Business.Dto;
using DAL.Models;

namespace Business;

public class BusinessMappingProfile : Profile
{
    public BusinessMappingProfile()
    {
        CreateMap<Match, MatchDto>()
            .ForMember(s=>s.ParticipationA,opt=>opt.MapFrom(src=>src.Participations.Count>=2 ? src.Participations[0]:null))
            .ForMember(s=>s.ParticipationB,opt=>opt.MapFrom(src=>src.Participations.Count>=2 ? src.Participations[1]:null));
        CreateMap<Participation, ParticipationDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
    }
}