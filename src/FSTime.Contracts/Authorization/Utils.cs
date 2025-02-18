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
            ValidIssuer = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secret)),

        };

        return p;
    }
}
