using ErrorOr;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.CompanyAggregate;
using MediatR;

namespace FSTime.Application.Companies.Commands;

public static class CreateCompany
{
    public record Command(Guid TenantId, string Name) : IRequest<ErrorOr<Guid>>;

    internal sealed class Handler(ITenantRepository tenantRepository, ICompanyRepository companyRepository) : IRequestHandler<Command, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                //Prüfen ob der Teneant Lizensiert ist
                var tenant = await tenantRepository.GetTenantById(request.TenantId);
                if (tenant is null) return CompanyErrors.Create_Company_Tenant_NotFound();
                if (!tenant.IsLicensed) return CompanyErrors.Create_Company_Tenant_NotLicences();
                
                //Prüfen ob es die Firma in diesem Tenant schon gibt
                var companies = await companyRepository.GetCompaniesByTenant(request.TenantId);
                if (companies.Any(x => x.Name == request.Name))
                    return CompanyErrors.Create_Company_Exists(request.Name);
                
                var company = new Company(request.Name, request.TenantId);
                var result = await companyRepository.CreateCompany(company);

                return result.Id;
            }
            catch (Exception e)
            {
                return CompanyErrors.Create_Company_Error(e.Message);
            }
        }
    }
}