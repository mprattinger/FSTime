using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Authorization;

namespace FSTime.Application.Authorization.Commands;
public static class RefreshToken
{
    public record Command(string refreshToken) : ICommand<RefreshTokenResponse>;

    internal sealed class Handler(ITokenService tokenService, IUserRepository userRepository) : ICommandHandler<Command, RefreshTokenResponse>
    {
        public async Task<ErrorOr<RefreshTokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (tokenService.TryValidateToken(request.refreshToken, out var userId, out var tenantId))
            {
                if (userId is null)
                {
                    return Error.Unauthorized();
                }

                var id = Guid.Parse(userId);
                var user = await userRepository.GetUserById(id);
                if (user is null)
                {
                    return Error.Unauthorized();
                }

                var tokenResult = tokenService.GenerateToken(user, tenantId);
                return new RefreshTokenResponse(user.UserName, tokenResult.AccessToken, tokenResult.AccessTokenExpiryDate);
            }
            return Error.Unauthorized();
        }
    }
}