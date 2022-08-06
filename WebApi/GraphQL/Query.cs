using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.GraphQL;

public class Query
{
    // [UsePaging()]
    [UseProjection()]
    // [UseFiltering()]
    // [UseSorting()]
    public IQueryable<MatchDto> Dto(MatchMakingContext context, [Service] IMapper mapper) =>
        context.Matches.ProjectTo<MatchDto>(mapper.ConfigurationProvider);
    
    
    // [UsePaging()]
    [UseProjection()]
    // [UseFiltering()]
    // [UseSorting()]
    public IQueryable<Match> Matches(MatchMakingContext context) =>
        context.Matches.Include(s=>s.Participations).ThenInclude(t=>t.Player);

}
