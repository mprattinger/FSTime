using FSTime.Application.Common.Interfaces;
using FSTime.Domain.AuthorizationAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class PermissionRepository(FSTimeDbContext dbContext) : IPermissionRepository
{
    public Task<List<Permission>> GetPermissions(Guid tenantId, Guid userId)
    {
        return dbContext
            .Permissions
            .Where(x => x.TenantId == tenantId && x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Permission> SetPermission(Permission permission)
    {
        dbContext.Permissions.Add(permission);
        await dbContext.SaveChangesAsync();
        return permission;
    }

    public async Task RemoveAllPermissions(Guid tenantId, Guid userId)
    {
        var permissions = dbContext.Permissions
            .Where(x => x.TenantId == tenantId && x.UserId == userId);

        dbContext.Permissions.RemoveRange(permissions);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemovePermission(Guid tenantId, Guid userId, string group, PermissionAction? action = null)
    {
        var permissions = dbContext.Permissions
            .Where(x => x.TenantId == tenantId && x.UserId == userId && x.Group == group);

        if (action != null) permissions = permissions.Where(x => x.Action == action);

        dbContext.Permissions.RemoveRange(permissions);
        await dbContext.SaveChangesAsync();
    }

    public Task<bool> HasPermission(Guid tenantId, Guid userId, string group, PermissionAction? action)
    {
        var permissions = dbContext.Permissions
            .Where(x => x.TenantId == tenantId && x.UserId == userId && x.Group == group);

        if (action != null) permissions = permissions.Where(x => x.Action == action);

        return permissions.AnyAsync();
    }

    public Task<List<string>> GetGroups()
    {
        return Task.FromResult(new List<string>());
    }

    public Task<List<string>> GetActions(string group)
    {
        var actions = Enum.GetNames(typeof(PermissionAction))
            .ToList();
        return Task.FromResult(actions);
    }
}