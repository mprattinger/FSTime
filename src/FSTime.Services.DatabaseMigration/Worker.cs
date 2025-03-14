using System.Diagnostics;
using FSTime.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace FSTime.Services.DatabaseMigration;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    internal const string ActivityName = "FSTimeMigrations";
    private static readonly ActivitySource ActivitySource = new(ActivityName);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

       try
       {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FSTimeDbContext>();

            await dbContext.Database.MigrateAsync();
       }
       catch (Exception e)
       {
           logger.LogError(e, "Error when migration the database: {0}", e.Message);
           activity?.RecordException(e);
           throw;
       }
       
       hostApplicationLifetime.StopApplication();
    }
}