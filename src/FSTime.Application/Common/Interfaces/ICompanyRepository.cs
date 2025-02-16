using FSTime.Domain.CompanyAggregate;

namespace FSTime.Application.Common.Interfaces;

public interface ICompanyRepository
{
    Task<Company> CreateCompany(Company company);
}
