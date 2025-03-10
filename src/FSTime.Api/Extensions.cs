using FSTime.Contracts.Authorization;
using FSTime.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FSTime.Api;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConf = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (jwtConf is not null)
        {
            services.AddSingleton(jwtConf);
        }

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = Utils.GetTokenValidationParameters(jwtConf?.Issuer!, jwtConf?.Audience!, jwtConf?.Secret!);
                options.MapInboundClaims = false;
            });

        services.AddAuthorization();

        return services;
    }
}
