using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants.Queries;

public static class GetTenantById
{
    public record Query(Guid tenantId) : IQuery<Tenant>;

    internal sealed class Handler(ITenantRepository tenantRepository) : IQueryHandler<Query, Tenant>
    {
        public async Task<ErrorOr<Tenant>> Handle(Query query, CancellationToken cancellationToken)
        {
            var t = await tenantRepository.GetTenantById(query.tenantId);
            if (t is null)
            {
                return TenantErrors.Tenant_ById_NotFound();
            }

            return t;
        }
    }
}