using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.CompanyAggregate;

namespace FSTime.Application.Companies.Queries;

public static class GetCompaniesByTenant
{
    public record Query(Guid TenantId) : IRequest<ErrorOr<List<Company>>>;

    internal sealed class Handler(ICompanyRepository companyRepository) : IRequestHandler<Query, ErrorOr<List<Company>>>
    {
        public async Task<ErrorOr<List<Company>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await companyRepository.GetCompaniesByTenant(request.TenantId);

                return companies;
            }
            catch (Exception e)
            {
                return CompanyErrors.Get_Company_By_Tenant_Error(e.Message);
            }
        }
    }
}