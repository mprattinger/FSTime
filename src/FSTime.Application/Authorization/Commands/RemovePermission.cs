using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Commands;

public static class RemovePermission
{
    public record Command(Guid TenantId, Guid UserId, string Group, PermissionAction? action)
        : ICommand<List<Permission>>;

    internal sealed class Handler(IPermissionRepository permissionRepository)
        : ICommandHandler<Command, List<Permission>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Command request, CancellationToken cancellationToken)
        {
            await permissionRepository.RemovePermission(request.TenantId, request.UserId, request.Group,
                request.action);

            var permissionsList = await permissionRepository.GetPermissions(request.TenantId, request.UserId);
            return permissionsList;
        }
    }
}