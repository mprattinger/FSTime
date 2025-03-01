using ErrorOr;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants;

public static class TenantErrors
{
    public static Error Creation_Error(string tenantName, string error) => Error.Conflict("TENANT_HANDLER.GEN_ERROR",
        $"When creating the tenant {tenantName} an error occured: {error}");
    
    public static Error Tenant_Lookup_Error(string error) => Error.Conflict("TENANT_HANDLER.GEN_ERROR",
        $"When trying to get the tenant an error occured: {error}");

    public static ErrorOr<Tenant> Tenant_Lookup_UserId_NotFound() => Error.NotFound("TENANT_HANDLER.WITH_USERID_NOT_FOUND",
        $"When trying to get the tenant by user id, the tenant was not found");
}