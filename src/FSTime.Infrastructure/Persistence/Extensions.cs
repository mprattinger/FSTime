using FSTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FSTime.Infrastructure.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistent(this IServiceCollection services)
    {
        services.AddDbContext<FSTimeDbContext>(options =>
        options.UseSqlite("Data Source=fstime.db"));

        services.AddRepositories();

        return services;
    }
}
