using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.TenantAggregate;
using FSTime.Domain.UserAggregate;
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
        
        builder.OwnsMany<TenantRole>(x => x.Users, tr =>
        {
            tr.ToTable("TenantRoles");
            tr.HasKey(t => new { t.TenantId, t.UserId });
            
            tr.Property(t => t.TenantId).ValueGeneratedNever();
            tr.Property(t => t.UserId).ValueGeneratedNever();
            tr.Property(t => t.RoleName);

            tr.WithOwner().HasForeignKey(r => r.TenantId);
            
            // tr.HasOne<User>()
            //     .WithOne()
            //     .HasForeignKey<TenantRole>(x => x.UserId);
        });

    }
}