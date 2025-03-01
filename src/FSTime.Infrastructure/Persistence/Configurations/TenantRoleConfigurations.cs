using FSTime.Domain.Common.ValueObjects;
using FSTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSTime.Infrastructure.Persistence.Configurations;

// public class TenantRoleConfigurations : IEntityTypeConfiguration<TenantRole>
// {
//     public void Configure(EntityTypeBuilder<TenantRole> builder)
//     {
//         builder.HasKey(t => new { t.TenantId, t.UserId });
//         
//         builder.Property(t => t.TenantId);
//         builder.Property(t => t.UserId);
//         builder.Property(t => t.RoleName);
//
//         builder.HasOne<User>(x => x.User);
//     }
// }‚