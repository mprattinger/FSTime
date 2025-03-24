using FSTime.Application.Common.Models;
using FSTime.Domain.TenantAggregate;
using FSTime.Domain.UserAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITokenService
{
    TokenGeneratorResult GenerateToken(User user, Guid? tenantId);
    bool TryValidateToken(string token, out string? userId, out Guid? tenantId);
    string GenerateEmailVerificationToken();
}
