using FSTime.Application.Common.Interfaces;
using FSTime.Domain.CompanyAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence.Repositories;

public class CompanyRepository(FSTimeDbContext context) : ICompanyRepository
{
    public async Task<Company> CreateCompany(Company company)
    {
        context.Companies.Add(company);
        await context.SaveChangesAsync();

        return company;
    }

    public async Task<List<Company>> GetCompaniesByTenant(Guid tenantId)
    {
        return await context.Companies.Where(x => x.TenantId == tenantId).ToListAsync();
    }

    public async Task<Company?> GetCompanyById(Guid companyId)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
    }
}