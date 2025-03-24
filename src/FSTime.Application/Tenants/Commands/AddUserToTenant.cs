using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Contracts.Common.Exceptions.Tenants;
using FSTime.Domain.TenantAggregate;
using MediatR;

namespace FSTime.Application.Tenants.Commands;

public static class AddUserToTenant
{
    public record Command(Guid TenantId, Guid UserId, string Role) : IRequest<ErrorOr<Tenant>>;

    internal sealed class Handler(ITenantRepository tenantRepository) : IRequestHandler<Command, ErrorOr<Tenant>>
    {
        public async Task<ErrorOr<Tenant>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await tenantRepository.AssignUserToTenant(request.TenantId, request.UserId, request.Role);
                return tenant;
            }
            catch (TenantNotFoundException)
            {
                return TenantErrors.Tenant_ById_NotFound();
            }
            catch (UserAlreadyAsignedException)
            {
                return TenantErrors.Tenant_User_Already_Assigned(request.TenantId, request.UserId);
            }
            catch (Exception e)
            {
                return TenantErrors.Tenant_AssignUser_Error(e.Message);
            }
        }
    }
}