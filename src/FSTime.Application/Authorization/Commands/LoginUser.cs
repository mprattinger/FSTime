using ErrorOr;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FSTime.Application.Authorization.Commands;

public static class LoginUser
{
    public record Command(string UserName, string Password) : IRequest<ErrorOr<LoginResponse>>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    internal sealed class Handler(IValidator<Command> validator, IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenGeneratorService) : IRequestHandler<Command, ErrorOr<LoginResponse>>
    {
        public async Task<ErrorOr<LoginResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var validation = await validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return validation.Errors.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));
                }

                if (!await userRepository.UserExists(request.UserName))
                {
                    return AuthorizationErrors.Login_Not_Valid();
                }
                
                var user = await userRepository.GetUser(request.UserName);
                if (user is null)
                {
                    return AuthorizationErrors.Login_Not_Valid();
                }

                if (!user.Verified)
                {
                    return AuthorizationErrors.User_Not_Verified();
                }
                
                var pwResult = passwordService.VerifyPassword(user.Password, request.Password);
                if (pwResult == PasswordVerificationResult.Failed)
                {
                    return AuthorizationErrors.Login_Not_Valid();
                }

                var tResult = tokenGeneratorService.GenerateToken(user);

                return new LoginResponse(user.UserName, tResult.AccessToken, tResult.AccessTokenExpiryDate, tResult.RefreshToken, tResult.RefreshTokenExpiryDate);
            }
            catch (Exception ex)
            {
                return Error.Forbidden("USER_LOGIN.GEN_ERROR", ex.Message);
            }
        }
    }
}
