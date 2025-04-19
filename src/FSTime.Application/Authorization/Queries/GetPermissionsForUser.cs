using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;
using MediatR;

namespace FSTime.Application.Authorization.Queries;

public static class GetPermissionsForUser
{
    public record Query(Guid tenantId, Guid UserId) : IRequest<ErrorOr<List<Permission>>>;

    internal sealed class Handler(IPermissionRepository permissionRepository)
        : IRequestHandler<Query, ErrorOr<List<Permission>>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var permissions = await permissionRepository.GetPermissions(request.tenantId, request.UserId);
                if (!permissions.Any()) return AuthorizationErrors.NoPermissions();

                return permissions;
            }
            catch (Exception e)
            {
                return AuthorizationErrors.Permissions_GenError(e.Message);
            }
        }
    }
}