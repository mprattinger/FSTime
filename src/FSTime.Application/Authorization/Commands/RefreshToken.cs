using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Authorization;
using MediatR;

namespace FSTime.Application.Authorization.Commands;

public static class RefreshToken
{
    public record Command(string refreshToken) : IRequest<ErrorOr<RefreshTokenResponse>>;

    internal sealed class Handler(ITokenService tokenService, IUserRepository userRepository) : IRequestHandler<Command, ErrorOr<RefreshTokenResponse>>
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
