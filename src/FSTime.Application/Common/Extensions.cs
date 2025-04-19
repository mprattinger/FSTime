using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FSTime.Application.Common;

public static class Extensions
{
    public static Guid? GetTenantIdFromHttpContext(this HttpContext httpContext)
    {
        var tenantClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == "TENANT");
        if (tenantClaim is null) return null;

        if (!Guid.TryParse(tenantClaim.Value, out var tenantId)) return null;

        return tenantId;
    }

    public static Guid? GetTenantIdFromAuthContext(this AuthorizationHandlerContext httpContext)
    {
        var tenantClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == "TENANT");
        if (tenantClaim is null) return null;

        if (!Guid.TryParse(tenantClaim.Value, out var tenantId)) return null;

        return tenantId;
    }
}