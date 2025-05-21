using ErrorOr;
using FlintSoft.CQRS;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.CompanyAggregate;

namespace FSTime.Application.Companies.Queries;

public static class GetCompaniesByTenant
{
    public record Query(Guid TenantId) : IQuery<List<Company>>;

    internal sealed class Handler : IQueryHandler<Query, List<Company>>
    {
        private readonly ICompanyRepository _companyRepository;

        public Handler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<ErrorOr<List<Company>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetCompaniesByTenant(request.TenantId);

            return companies;

        }
    }
}