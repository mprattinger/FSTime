using System.Reflection;
using FSTime.Domain.CompanyAggregate;
using FSTime.Domain.EmployeeAggregate;
using FSTime.Domain.TenantAggregate;
using FSTime.Domain.UserAggregate;
using FSTime.Domain.WorkScheduleAggregate;
using Microsoft.EntityFrameworkCore;

namespace FSTime.Infrastructure.Persistence;

public class FSTimeDbContext : DbContext
{
    public FSTimeDbContext(DbContextOptions<FSTimeDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}