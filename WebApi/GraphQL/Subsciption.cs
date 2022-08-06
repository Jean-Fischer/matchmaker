using Business.Dto;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace WebApi.GraphQL;

public class Subsciption
{
    [Subscribe]
    [Topic("RefreshMatches")]
    public List<MatchDto> RefreshMatches([EventMessage] List<MatchDto> matchDto) => matchDto;

}