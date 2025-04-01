using FSTime.Domain.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CompanyId);
        builder.Property(x => x.FirstName);
        builder.Property(x => x.LastName);
        builder.Property(x => x.MiddleName);
        builder.Property(x => x.EmployeeCode);
        builder.Property(x => x.EntryDate);
        builder.Property(x => x.UserId);
        builder.Property(x => x.SupervisorId);
        builder.Property(x => x.IsHead);

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Employee)
            .HasForeignKey<Employee>(x => x.UserId);

        builder.HasOne(x => x.Supervisor)
            .WithMany()
            .HasForeignKey(x => x.SupervisorId);

        builder.OwnsMany(x => x.Workschedules, ew =>
        {
            ew.ToTable("EmployeeWorkschedules");
            ew.HasKey(t => new { t.EmployeeId, t.WorkscheduleId, t.From });

            ew.Property(t => t.EmployeeId).ValueGeneratedNever();
            ew.Property(t => t.WorkscheduleId).ValueGeneratedNever();
            ew.Property(t => t.From).IsRequired();
            ew.Property(t => t.To).IsRequired(false);

            ew.WithOwner().HasForeignKey(r => r.EmployeeId);

            ew.HasOne(x => x.Employee)
                .WithMany(e => e.Workschedules)
                .HasForeignKey(x => x.EmployeeId);

            ew.HasOne(x => x.Workschedule)
                .WithMany()
                .HasForeignKey(x => x.WorkscheduleId);
        });
    }
}