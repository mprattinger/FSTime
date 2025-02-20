using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;

namespace FSTime.Infrastructure.Persistence;

public class FSTimeDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<TenantRole> TenantRoles => Set<TenantRole>();
    
    public FSTimeDbContext(DbContextOptions<FSTimeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
