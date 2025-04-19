using FSTime.Domain.WorkScheduleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

public class WorkScheduleConfigurations : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CompanyId);

        builder.Property(x => x.Description);

        builder.Property(x => x.Monday);
        builder.Property(x => x.Tuesday);
        builder.Property(x => x.Wednesday);
        builder.Property(x => x.Thursday);
        builder.Property(x => x.Friday);
        builder.Property(x => x.Saturday);
        builder.Property(x => x.Sunday);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId);
    }
}