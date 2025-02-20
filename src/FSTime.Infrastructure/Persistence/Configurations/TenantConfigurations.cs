using FSTime.Domain.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

public class TenantConfigurations : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.IsLicensed);
    }
}