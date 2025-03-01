using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITenantRepository
{
    Task<Tenant> CreateTenant(Tenant tenant, TenantRole role);
    Task<Tenant?> GetTenantByUserId(Guid userId);
    Task<Tenant?> GetTenantById(Guid tenantId);
    Task<bool> UserHasTenantRole(Guid tenantId, Guid userId, string role);
}
