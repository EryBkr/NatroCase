using Core.SecurityIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<BaseUserTokenEntity>
{
    public void Configure(EntityTypeBuilder<BaseUserTokenEntity> builder)
    {
        // Composite primary key consisting of the UserId, LoginProvider and Name
        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        // Limit the size of the composite key columns due to common DB restrictions
        builder.Property(t => t.LoginProvider).HasMaxLength(256);
        builder.Property(t => t.Name).HasMaxLength(256);

        // Maps to the AspNetUserTokens table
        builder.ToTable("UserTokens");
    }
}
