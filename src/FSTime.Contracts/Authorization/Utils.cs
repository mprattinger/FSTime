using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FSTime.Contracts.Authorization;

public static class Utils
{
    public static TokenValidationParameters GetTokenValidationParameters(string issuer, string audience, string secret)
    {
        var p = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secret)),
        };
        return p;
    }

    public static Guid? GetTenantIdFromHttpContext(this HttpContext httpContext)
    {
        var tenantClaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == "TENANT");
        if (tenantClaim is null)
        {
            return null;
        }

        if (!Guid.TryParse(tenantClaim.Value, out var tenantId))
        {
            return null;
        }

        return tenantId;
    }
}