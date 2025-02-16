using FSTime.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Services;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        return services;
    }
}
