using FSTime.Domain.AuthorizationAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface IPermissionRepository
{
    Task<List<Permission>> GetPermissions(Guid tenantId, Guid userId);

    Task<Permission> SetPermission(Permission permission);

    Task RemoveAllPermissions(Guid tenantId, Guid userId);

    Task RemovePermission(Guid tenantId, Guid userId, string group, PermissionAction? action = null);

    Task<bool> HasPermission(Guid tenantId, Guid userId, string group, PermissionAction? action = null);
}