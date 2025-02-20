using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class TenantRepository(FSTimeDbContext context) : ITenantRepository
{
    public async Task<Tenant> CreateTenant(Tenant tenant, TenantRole role)
    {
        context.Tenants.Add(tenant);
        context.TenantRoles.Add(role);
        await context.SaveChangesAsync();

        return tenant;
    }
    
}