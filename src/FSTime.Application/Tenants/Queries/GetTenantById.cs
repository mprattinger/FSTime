using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants.Queries;

public static class GetTenantById
{
    public record Query(Guid tenantId) : IRequest<ErrorOr<Tenant>>;

    internal sealed class Handler(ITenantRepository tenantRepository) : IRequestHandler<Query, ErrorOr<Tenant>>
    {
        public async Task<ErrorOr<Tenant>> Handle(Query query, CancellationToken cancellationToken)
        {
            try
            {
                var t = await tenantRepository.GetTenantById(query.tenantId);
                if (t is null)
                {
                    return TenantErrors.Tenant_ById_NotFound();
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