using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITenantRepository
{
    Task<Tenant> CreateTenant(Tenant tenant);
    Task<List<Tenant>> GetTenantsByUserId(Guid userId);
    Task<Tenant?> GetTenantById(Guid tenantId);
    Task<bool> TenantUserHasRole(Guid tenantId, Guid userId, string role);
    Task<Tenant> AssignUserToTenant(Guid tenantId, Guid userId, string role);
    Task<Tenant> UpdateUserTenantRole(Guid tenantId, Guid userId, string role);
    Task<bool> RemoveUserFromTenant(Guid tenantId, Guid userId);
    Task<bool> IsTenantUser(Guid tenantId, Guid userId);
}