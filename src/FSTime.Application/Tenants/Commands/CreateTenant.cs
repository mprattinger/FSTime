using ErrorOr;
using FluentValidation;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;
using MediatR;

namespace FSTime.Application.Tenants.Commands;

public static class CreateTenant
{
    public record Command(string Name, Guid UserId) : IRequest<ErrorOr<Guid>>;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    internal sealed class Handler(IValidator<Command> validator, ITenantRepository tenantRepository) : IRequestHandler<Command, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                return TenantErrors.Creation_Error(request.Name, e.Message);
            }
        }
    }
}