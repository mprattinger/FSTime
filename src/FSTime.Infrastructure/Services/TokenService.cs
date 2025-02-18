using FSTime.Application.Common.Interfaces;
using FSTime.Application.Common.Models;
using FSTime.Contracts.Authorization;
using FSTime.Domain.Common.Interfaces;
using FSTime.Domain.UserAggregate;
using FSTime.Infrastructure.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FSTime.Infrastructure.Services;

public class TokenService(IDateTimeProvider dateTimeProvider, JwtSettings jwtSettings) : ITokenService
{
    public TokenGeneratorResult GenerateToken(User user)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim("UMUID", user.Id.ToString())
        };

        var expiresAT = dateTimeProvider.UtcNow.AddMinutes(jwtSettings.AccessTokeExpiryMinutes ?? 0);
        var expiresRT = dateTimeProvider.UtcNow.AddMinutes(jwtSettings.RefreshTokeExpiryMinutes ?? 0);

        var at = generateTokenInternal(claims, expiresAT);
        var rt = generateTokenInternal(claims, expiresRT);

        return new TokenGeneratorResult(at, expiresAT, rt, expiresRT);
    }

    public bool TryValidateToken(string token, out string? userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, Utils.GetTokenValidationParameters(jwtSettings.Issuer!, jwtSettings.Audience!, jwtSettings.Secret!), out var securityToken);
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            userId = userIdClaim?.Value;
            return true;
        }
        catch
        {
            userId = null;
            return false;
            throw;
        }
    }

    string generateTokenInternal(Claim[] claims, DateTime expires)
    {
        var signingCredentials = new SigningCredentials(
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret ?? "")),
           SecurityAlgorithms.HmacSha256
       );

        var secToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            expires: expires
        );

        return new JwtSecurityTokenHandler().WriteToken(secToken);
    }
}
