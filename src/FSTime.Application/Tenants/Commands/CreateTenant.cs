using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants.Commands;

public static class CreateTenant
{
    public record Command(string Name, Guid UserId) : ICommand<Guid>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    internal sealed class Handler(IValidator<Command> validator, ITenantRepository tenantRepository) : ICommandHandler<Command, Guid>
    {
        public async Task<ErrorOr<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                return validation.Errors.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));
            }

            var tenantGuid = Guid.CreateVersion7();
            var tenant = new Tenant(request.Name, tenantGuid);
            var role = new TenantRole(tenantGuid, request.UserId, "ADMIN");
            tenant.AddUser(role);
            var result = await tenantRepository.CreateTenant(tenant);
            return result.Id;

        }
    }
}