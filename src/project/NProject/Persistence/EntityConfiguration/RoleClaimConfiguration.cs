using Core.SecurityIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<BaseRoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<BaseRoleClaimEntity> builder)
    {
        // Primary key
        builder.HasKey(rc => rc.Id);

        // Maps to the AspNetRoleClaims table
        builder.ToTable("RoleClaims");
    }
}
