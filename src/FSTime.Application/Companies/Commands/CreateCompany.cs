using ErrorOr;
using FlintSoft.CQRS.Handlers;
using FlintSoft.CQRS.Interfaces;
using FSTime.Application.Common.Interfaces;
using FSTime.Domain.CompanyAggregate;

namespace FSTime.Application.Companies.Commands;

public static class CreateCompany
{
    public record Command(Guid TenantId, string Name) : ICommand<Guid>;

    internal sealed class Handler(ITenantRepository tenantRepository, ICompanyRepository companyRepository) : ICommandHandler<Command, Guid>
    {
        public async Task<ErrorOr<Guid>> Handle(Command request, CancellationToken cancellationToken)
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
    }
}