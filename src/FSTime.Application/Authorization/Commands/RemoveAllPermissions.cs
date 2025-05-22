using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Authorization.Commands;

public static class RemoveAllPermissions
{
    public record Command(Guid TenantId, Guid UserId) : ICommand<List<Permission>>;

    internal sealed class Handler(IPermissionRepository permissionRepository)
        : ICommandHandler<Command, List<Permission>>
    {
        public async Task<ErrorOr<List<Permission>>> Handle(Command request, CancellationToken cancellationToken)
        {
            await permissionRepository.RemoveAllPermissions(request.TenantId, request.UserId);

            var permissionsList = await permissionRepository.GetPermissions(request.TenantId, request.UserId);
            return permissionsList;
        }
    }
}