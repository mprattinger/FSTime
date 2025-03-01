using FSTime.Domain.TenantAggregate;
using MediatR;
using ErrorOr;
using FSTime.Application.Common.Interfaces;

namespace FSTime.Application.Tenants.Queries;

public static class GetUserTenant
{
    public record Query(Guid userId) : IRequest<ErrorOr<Tenant>>;
    
    internal sealed class Handler(ITenantRepository tenantRepository) : IRequestHandler<Query, ErrorOr<Tenant>>
    {
        public async Task<ErrorOr<Tenant>> Handle(Query query, CancellationToken cancellationToken)
        {
            try
            {
                var t = await tenantRepository.GetTenantByUserId(query.userId);
                if (t is null)
                {
                    return TenantErrors.Tenant_Lookup_UserId_NotFound();
                }

                return t;
            }
            catch (Exception e)
            {
                return TenantErrors.Tenant_Lookup_Error(e.Message);
            }
        }
    }
}