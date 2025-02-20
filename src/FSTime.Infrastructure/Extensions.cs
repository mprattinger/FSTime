using FSTime.Infrastructure.Auth;
using FSTime.Infrastructure.Persistence;
using FSTime.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistent(configuration);
        services.AddServices();

        services.AddAuth(configuration);

        return services;
    }
}
