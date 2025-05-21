using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Queries;

public static class GetActions
{
    public record Query(string Group) : IQuery<List<string>>;

    internal sealed class Handler : IQueryHandler<Query, List<string>>
    {
        public async Task<ErrorOr<List<string>>> Handle(Query request, CancellationToken cancellationToken = default)
        {
            var actions = new List<string>();

            switch (request.Group)
            {
                case "EMPLOYEE":
                    actions.Add(PermissionAction.Read.ToString());
                    actions.Add(PermissionAction.Update.ToString());
                    actions.Add(PermissionAction.Delete.ToString());
                    break;

                case "WORKSCHEDULE":
                    actions.Add(PermissionAction.Read.ToString());
                    actions.Add(PermissionAction.Update.ToString());
                    actions.Add(PermissionAction.Delete.ToString());
                    break;

                default:
                    return AuthorizationErrors.GetActions_InvalidGroup(request.Group);// Return an error if the group is not valid
            }

            return await Task.FromResult<ErrorOr<List<string>>>(actions);
        }
    }
}
