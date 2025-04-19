using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Common.Exceptions.Tenants;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class TenantRepository(FSTimeDbContext context) : ITenantRepository
{
    public async Task<Tenant> CreateTenant(Tenant tenant)
    {
        context.Tenants.Add(tenant);
        // context.TenantRoles.Add(role);
        await context.SaveChangesAsync();

        return tenant;
    }

    public async Task<List<Tenant>> GetTenantsByUserId(Guid userId)
    {
        var tenants = await context.Set<Tenant>()
            .Where(x => x.Users.Any(y => y.UserId == userId))
            .Include(x => x.Users)
            .ToListAsync();
        return tenants;
    }

    public async Task<Tenant?> GetTenantById(Guid tenantId)
    {
        return await context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
    }

    public async Task<bool> TenantUserHasRole(Guid tenantId, Guid userId, string role)
    {
        return await context.Tenants.AnyAsync(x =>
            x.Id == tenantId && x.Users.Any(y => y.UserId == userId && y.RoleName == role));
    }

    public async Task<Tenant> AssignUserToTenant(Guid tenantId, Guid userId, string role)
    {
        var tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        if (tenant is null) throw new TenantNotFoundException(tenantId);

        if (tenant.Users.Any(x => x.UserId == userId)) throw new UserAlreadyAsignedException(tenant.Id, userId);

        tenant.AddUser(new TenantRole(tenantId, userId, role));
        context.Tenants.Update(tenant);
        await context.SaveChangesAsync();

        return tenant;
    }

    public async Task<Tenant> UpdateUserTenantRole(Guid tenantId, Guid userId, string role)
    {
        var tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        if (tenant is null) throw new TenantNotFoundException(tenantId);

        var user = tenant.Users.FirstOrDefault(x => x.UserId == userId);
        if (user is null) throw new UserNotAssignedException(tenant.Id, userId);

        tenant.UpdateUser(new TenantRole(tenantId, userId, role));
        context.Tenants.Update(tenant);
        await context.SaveChangesAsync();

        return tenant;
    }

    public async Task<bool> RemoveUserFromTenant(Guid tenantId, Guid userId)
    {
        var tenant = await context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        if (tenant is null) throw new TenantNotFoundException(tenantId);

        var user = tenant.Users.FirstOrDefault(x => x.UserId == userId);
        if (user is null) throw new UserNotAssignedException(tenant.Id, userId);

        tenant.RemoveUser(userId);
        context.Tenants.Update(tenant);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsTenantUser(Guid tenantId, Guid userId)
    {
        return await context
            .Tenants
            .AnyAsync(x => x.Id == tenantId && x.Users.Any(y => y.UserId == userId));
    }
}