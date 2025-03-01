using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class TenantRepository(FSTimeDbContext context) : ITenantRepository
{
    public async Task<Tenant> CreateTenant(Tenant tenant, TenantRole role)
    {
        context.Tenants.Add(tenant);
        // context.TenantRoles.Add(role);
        await context.SaveChangesAsync();

        return tenant;
    }
    
    public async Task<Tenant?> GetTenantByUserId(Guid userId)
    {
        var tenant = await context.Set<Tenant>()
            .Where(x => x.Users.Any(y => y.UserId == userId))
            .Include(x => x.Users)
            .FirstOrDefaultAsync();
        return tenant;
    }

    public async Task<Tenant?> GetTenantById(Guid tenantId)
    {
        return await context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
    }

    public async Task<bool> UserHasTenantRole(Guid tenantId, Guid userId, string role)
    {
        return await context.Tenants.AnyAsync(x => x.Id == tenantId && x.Users.Any(y => y.UserId == userId && y.RoleName == role));
    }
}