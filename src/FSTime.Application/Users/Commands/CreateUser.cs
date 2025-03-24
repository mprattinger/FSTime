using ErrorOr;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Users;
using FSTime.Domain.UserAggregate;
using MediatR;

namespace FSTime.Application.Users.Commands;

public static class CreateUser
{
    public record Command(string username, string password, string email) : IRequest<ErrorOr<RegisterUserResult>>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.username).NotEmpty();
            RuleFor(x => x.password).NotEmpty();
            RuleFor(x => x.email).NotEmpty().EmailAddress();
        }
    }

    internal sealed class Handler(IUserRepository userRepository, IPasswordService passwordService, IValidator<Command> validator, ITokenService tokenService) : IRequestHandler<Command, ErrorOr<RegisterUserResult>>
    {
        public async Task<ErrorOr<RegisterUserResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var validation = await validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return validation.Errors.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));
                }

                if (await userRepository.UserExists(request.username))
                {
                    return UserErrors.Already_Exists(request.username);
                }

                var pw = passwordService.HashPassword(request.password);
                var verifyToken = tokenService.GenerateEmailVerificationToken();
                
                var user = new User(request.username, pw.password, request.email, pw.salt, verifyToken, DateTime.UtcNow.AddHours(2));

                var res = await userRepository.AddUser(user);
                
                var ret  = new RegisterUserResult(res.Id, verifyToken, res.Email);
                return ret;
            }
            catch (Exception ex)
            {
                return UserErrors.Creation_Error(request.username, ex.Message);
            }
        }
    }
}
