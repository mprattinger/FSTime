using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.UserAggregate;
using MediatR;

namespace FSTime.Application.Users.Commands;

public static class CreateUser
{
    public record Command(string username, string password, string email) : IRequest<ErrorOr<Guid>>;

    internal sealed class Handler(IUserRepository userRepository, IPasswordService passwordService) : IRequestHandler<Command, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                if (await userRepository.UserExists(request.username))
                {
                    return UserErrors.Already_Exists(request.username);
                }

                var pw = passwordService.HashPassword(request.password);

                var user = new User(request.username, pw, request.email);

                var res = await userRepository.AddUser(user);

                return res.Id;
            }
            catch (Exception ex)
            {
                return UserErrors.Creation_Error(request.username, ex.Message);
            }
        }
    }
}
