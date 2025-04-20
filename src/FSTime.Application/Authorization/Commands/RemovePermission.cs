using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Commands;

public static class RemovePermission
{
    public record Command(Guid TenantId, Guid UserId, string Group, PermissionAction? action)
        : IRequest<ErrorOr<List<Permission>>>;

    internal sealed class Handler(IPermissionRepository permissionRepository)
        : IRequestHandler<Command, ErrorOr<List<Permission>>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                await permissionRepository.RemovePermission(request.TenantId, request.UserId, request.Group,
                    request.action);

                var permissionsList = await permissionRepository.GetPermissions(request.TenantId, request.UserId);
                return permissionsList;
            }
            catch (Exception e)
            {
                return AuthorizationErrors.Permissions_GenError(e.Message);
            }
        }
    }
}