using ErrorOr;
using FlintSoft.CQRS;

namespace FSTime.Application.Authorization.Queries;

public static class GetGroups
{
    public record Query() : IRequest<ErrorOr<List<string>>>;

    internal sealed class Handler() : IRequestHandler<Query, ErrorOr<List<string>>>
    {
        public Task<ErrorOr<List<string>>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<ErrorOr<List<string>>>(new List<string> { "EMPLOYEE", "WORKSCHEDULE" });
        }
    }
}