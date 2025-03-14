using FSTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FSTime.Infrastructure.Persistence;

public static class Extensions
{
    public static IHostApplicationBuilder? AddPersistent(this IHostApplicationBuilder? host)
    {
        host?.AddNpgsqlDbContext<FSTimeDbContext>("fstimedb");
        // services.AddDbContext<FSTimeDbContext>(options =>
        // //options.UseSqlite("Data Source=fstime.db")
        //     options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        // );

        host?.Services.AddRepositories();

        return host;
    }
}
