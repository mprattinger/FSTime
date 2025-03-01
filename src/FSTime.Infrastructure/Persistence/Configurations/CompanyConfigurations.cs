using FSTime.Domain.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

public class CompanyConfigurations: IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(x => x.Name);
        
        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => x.TenantId);
    }
}