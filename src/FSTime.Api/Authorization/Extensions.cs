using FSTime.Contracts.Authorization;
using FSTime.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FSTime.Api;

public static class Extensions
{
    public static IHostApplicationBuilder? AddAuth(this IHostApplicationBuilder? host)
    {
        var jwtConf = host?.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (jwtConf is not null)
        {
            host?.Services.AddSingleton(jwtConf);
        }

        var fsTimeSecurity = host?.Configuration.GetSection("FSTimeSecurity").Get<FSTimeSecuritySettings>();
        if (fsTimeSecurity is not null)
        {
            host?.Services.AddSingleton(fsTimeSecurity);
        }
        
        // host?.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.TokenValidationParameters = Utils.GetTokenValidationParameters(jwtConf?.Issuer!, jwtConf?.Audience!, jwtConf?.Secret!);
        //         options.MapInboundClaims = false;
        //     });

        host?.Services.AddAuthorization();

        return host;
    }
}
