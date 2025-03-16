using FSTime.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FSTime.Contracts.Authorization;
using FSTime.Infrastructure.Authorization.Permissions;
using Microsoft.Extensions.Hosting;

namespace FSTime.Infrastructure.Authorization;

public static class Extensions
{
    public static IHostApplicationBuilder? AddAuthorization(this IHostApplicationBuilder? host)
    {
        var jwtConf = host?.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (jwtConf is not null)
        {
            host?.Services.AddSingleton(jwtConf);
        }

        host?.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = Utils.GetTokenValidationParameters(jwtConf?.Issuer!, jwtConf?.Audience!, jwtConf?.Secret!);
                options.MapInboundClaims = false;

                // options.Events = new JwtBearerEvents
                // {
                //     OnMessageReceived = ctx =>
                //     {
                //         if (ctx.Request.Cookies.TryGetValue("refreshToken", out var token))
                //         {
                //             ctx.Token = token;
                //         }
                //         return Task.CompletedTask;
                //     }
                // };
            });

        host?.Services.AddAuthorization();

        host?.Services.AddPermissions();
        
        return host;
    }
}
