using FSTime.Application.Common.Models;
using FSTime.Domain.UserAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ITokenService
{
    TokenGeneratorResult GenerateToken(User user);
    bool TryValidateToken(string token, out string? userId);
}
