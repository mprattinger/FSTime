using ErrorOr;

namespace FSTime.Application.Tenants;

public static class TenantErrors
{
    public static Error Creation_Error(string tenantName, string error) => Error.Conflict("TENANT_HANDLER.GEN_ERROR",
        $"When creating the tenant {tenantName} an error occured: {error}");
}