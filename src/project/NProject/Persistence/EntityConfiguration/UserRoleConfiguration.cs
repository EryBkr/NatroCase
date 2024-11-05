using Core.SecurityIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<BaseUserRoleEntity>
{
    public void Configure(EntityTypeBuilder<BaseUserRoleEntity> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        builder.ToTable("UserRoles");

        builder.HasData(
            new BaseUserRoleEntity
            {
                UserId = 1,
                RoleId = 1

            });
    }
}
