using FSTime.Infrastructure.Persistence;
using FSTime.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistent();
        services.AddServices();

        return services;
    }
}
