using FSTime.Application.Common.Interfaces;
using FSTime.Domain.Common.Interfaces;
using FSTime.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FSTime.Infrastructure.Services;

public static class Extensions
{
    public static IHostApplicationBuilder? AddServices(this IHostApplicationBuilder? host)
    {
        var fsTimeSecurity = host?.Configuration.GetSection("FSTimeSecurity").Get<FSTimeSecuritySettings>();
        if (fsTimeSecurity is not null)
        {
            host?.Services.AddSingleton(fsTimeSecurity);
        }
        
        host?.Services.AddScoped<IPasswordService, PasswordService>();
        host?.Services.AddScoped<ITokenService, TokenService>();
        host?.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

        return host;
    }
}
