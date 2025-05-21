using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;

namespace FSTime.Application.Authorization.Queries;

public static class GetGroups
{
    public record Query() : IQuery<List<string>>;

    internal sealed class Handler(IPolicyInspector policyInspector) : IQueryHandler<Query, List<string>>
    {
        public Task<ErrorOr<List<string>>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            var groups = policyInspector.GetGroups();

            //return Task.FromResult<ErrorOr<List<string>>>(new List<string> { "EMPLOYEE", "WORKSCHEDULE" });
            return Task.FromResult<ErrorOr<List<string>>>(groups);
        }
    }
}