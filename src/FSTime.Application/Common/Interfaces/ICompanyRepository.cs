using FSTime.Domain.CompanyAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ICompanyRepository
{
    Task<Company> CreateCompany(Company company);
    
    Task<List<Company>> GetCompaniesByTenant(Guid tenantId);
    
    Task<Company?> GetCompanyById(Guid companyId);
}