using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Application.Tenants.Queries;

public static class GetUserTenant
{
    public record Query(Guid userId) : IQuery<Tenant>;

    internal sealed class Handler(ITenantRepository tenantRepository) : IQueryHandler<Query, Tenant>
    {
        public async Task<ErrorOr<Tenant>> Handle(Query query, CancellationToken cancellationToken)
        {
            var t = await tenantRepository.GetTenantsByUserId(query.userId);
            if (t.Count == 0 || t.First() is null)
            {
                return TenantErrors.Tenant_Lookup_UserId_NotFound();
            }

            return t.First()!;
        }
    }
}