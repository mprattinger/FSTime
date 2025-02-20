using FSTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistent(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FSTimeDbContext>(options =>
        //options.UseSqlite("Data Source=fstime.db")
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddRepositories();

        return services;
    }
}
