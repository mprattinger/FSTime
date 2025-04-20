using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.TenantAggregate;

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
                var t = await tenantRepository.GetTenantsByUserId(query.userId);
                if (t.Count == 0 || t.First() is null)
                {
                    return TenantErrors.Tenant_Lookup_UserId_NotFound();
                }

                return t.First()!;
            }
            catch (Exception e)
            {
                return TenantErrors.Tenant_Lookup_Error(e.Message);
            }
        }
    }
}