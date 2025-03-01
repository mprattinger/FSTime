using FSTime.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Persistence.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        return services;
    }
}
