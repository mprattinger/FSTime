using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Services;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}
