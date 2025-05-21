using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;

namespace FSTime.Application.Users.Commands;

public static class VerifyUser
{
    public record Command(string token, string email) : ICommand<Success>;

    internal sealed class Handler(IUserRepository userRepository) : ICommandHandler<Command, Success>
    {
        public async Task<ErrorOr<Success>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByVerificationData(request.email, request.token);
            if (user is null) return UserErrors.User_Not_Found;

            if (user.VerifyTokenExpires < DateTime.UtcNow)
            {
                return UserErrors.Verification_Token_Expired;
            }

            user.SetVerified();
            await userRepository.UpdateUser(user);

            return new Success();
        }
    }
}