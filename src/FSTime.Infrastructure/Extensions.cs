using FSTime.Infrastructure.Auth;
using FSTime.Infrastructure.Persistence;
using FSTime.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FSTime.Infrastructure;

public static class Extensions
{
    public static IHostApplicationBuilder? AddInfrastructure(this IHostApplicationBuilder? host)
    {
        host?.AddPersistent();
        host?.Services.AddServices();

        host?.AddAuth();

        return host;
    }
}
