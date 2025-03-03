using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITenantRepository
{
    Task<Tenant> CreateTenant(Tenant tenant, TenantRole role);
    Task<List<Tenant>> GetTenantsByUserId(Guid userId);
    Task<Tenant?> GetTenantById(Guid tenantId);
    Task<bool> UserHasTenantRole(Guid tenantId, Guid userId, string role);
}
