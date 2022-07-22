using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Dto;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.GraphQL;

public class Query
{
    public IQueryable<MatchDto> Dto(MatchMakingContext context, [Service] IMapper mapper) =>
        context.Matches.ProjectTo<MatchDto>(mapper.ConfigurationProvider);

}
