using Core.SecurityIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class UserClaimConfiguration : IEntityTypeConfiguration<BaseUserClaimEntity>
{
    public void Configure(EntityTypeBuilder<BaseUserClaimEntity> builder)
    {
        // Primary key
        builder.HasKey(uc => uc.Id);

        // Maps to the AspNetUserClaims table
        builder.ToTable("UserClaims");
    }
}
