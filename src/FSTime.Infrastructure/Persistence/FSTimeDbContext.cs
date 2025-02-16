using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FSTime.Infrastructure.Persistence;

public class FSTimeDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public FSTimeDbContext(DbContextOptions<FSTimeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
