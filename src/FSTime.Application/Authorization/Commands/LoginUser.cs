using ErrorOr;
using FlintSoft.CQRS;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Authorization;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Authorization.Commands;

public static class LoginUser
{
    public record Command(string UserName, string Password, Guid? Tenant) : ICommand<LoginResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    internal sealed class Handler(IValidator<Command> validator, IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenGeneratorService, ITenantRepository tenantRepository)
        : ICommandHandler<Command, LoginResponse>
    {
        public async Task<ErrorOr<LoginResponse>> Handle(Command request, CancellationToken cancellationToken)
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

            var pwResult = passwordService.VerifyPassword(user.Password, user.Salt, request.Password);
            if (!pwResult)
            {
                return AuthorizationErrors.Login_Not_Valid();
            }

            //Check if any Tenants
            var tenants = await tenantRepository.GetTenantsByUserId(user.Id);
            if (tenants.Count > 1 && request.Tenant is null)
            {
                var info = new Dictionary<string, object>();
                tenants.ForEach(t => info.Add(t.Id.ToString(), t.Name));
                return Error.Custom(99, "USER_LOGIN.MULTIPLE_TENANTS", "Dem Benutzer sind mehrere Mandanten zugeordnet.", info);
            }

            Tenant? tenant = null;
            if (tenants.Count > 1 && request.Tenant is not null)
            {
                tenant = tenants.FirstOrDefault(x => x.Id == request.Tenant);
                if (tenant is null)
                {
                    return Error.NotFound("USER_LOGIN.GIVEN_TENANT_NOT_FOUND", "Der Mandant wurde nicht gefunden.");
                }
            }

            if (tenants.Count == 1)
            {
                tenant = tenants.First();
            }

            var tResult = tokenGeneratorService.GenerateToken(user, tenant?.Id);

            return new LoginResponse(user.UserName, tResult.AccessToken, tResult.AccessTokenExpiryDate, tResult.RefreshToken, tResult.RefreshTokenExpiryDate);
        }
    }
}