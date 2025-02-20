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
        builder.Property(x => x.Email);
        builder.Property(x => x.Verified);
    }
}
