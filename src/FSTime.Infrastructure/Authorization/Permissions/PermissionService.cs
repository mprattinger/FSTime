using FSTime.Infrastructure.Common.Interfaces;

namespace FSTime.Infrastructure.Authorization.Permissions;

public class PermissionService : IPermissionService
{
    public async Task<bool> HasPermission(Guid userId, string group, string action)
    {
        return await Task.FromResult(true);
    }
}