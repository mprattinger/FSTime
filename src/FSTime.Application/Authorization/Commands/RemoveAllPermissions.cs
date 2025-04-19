using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;
using MediatR;

namespace FSTime.Application.Authorization.Commands;

public static class RemoveAllPermissions
{
    public record Command(Guid TenantId, Guid UserId) : IRequest<ErrorOr<List<Permission>>>;

    internal sealed class Handler(IPermissionRepository permissionRepository)
        : IRequestHandler<Command, ErrorOr<List<Permission>>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                await permissionRepository.RemoveAllPermissions(request.TenantId, request.UserId);

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