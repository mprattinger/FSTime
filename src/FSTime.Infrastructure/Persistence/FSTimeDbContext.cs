using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.CompanyAggregate;
using FSTime.Domain.TenantAggregate;
using FSTime.Domain.WorkScheduleAggregate;

namespace FSTime.Infrastructure.Persistence;

public class FSTimeDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Company> Companies { get; set; }
    public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();
    
    public FSTimeDbContext(DbContextOptions<FSTimeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
