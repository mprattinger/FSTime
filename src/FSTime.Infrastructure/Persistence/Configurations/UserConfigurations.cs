using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

internal class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.UserName);
        builder.Property(x => x.Password);
        builder.Property(x => x.Salt);
        builder.Property(x => x.Email);
        builder.Property(x => x.Verified);
        builder.Property(x => x.VerifyToken);
        builder.Property(x => x.VerifyTokenExpires);

        builder.HasOne(x => x.Employee)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.EmployeeId);
    }
}