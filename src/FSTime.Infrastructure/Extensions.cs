using FSTime.Infrastructure.Authorization;
using FSTime.Infrastructure.Persistence;
using FSTime.Infrastructure.Services;
using Microsoft.Extensions.Hosting;

namespace FSTime.Infrastructure;

public static class Extensions
{
    public static IHostApplicationBuilder? AddInfrastructure(this IHostApplicationBuilder? host)
    {
        host?.AddPersistent();
        host?.AddServices();

        host?.AddAuthorization();

        return host;
    }
}
