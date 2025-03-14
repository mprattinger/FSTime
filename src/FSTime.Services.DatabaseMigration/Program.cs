using FSTime.Infrastructure.Persistence;
using FSTime.Services.DatabaseMigration;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<FSTimeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("fstimedb"));
});

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivityName));

var host = builder.Build();
host.Run();