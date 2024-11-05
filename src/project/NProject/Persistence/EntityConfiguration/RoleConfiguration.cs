using Core.SecurityIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<BaseRoleEntity>
{
    public void Configure(EntityTypeBuilder<BaseRoleEntity> builder)
    {
        // Primary key
        builder.HasKey(rc => rc.Id);

        // Maps to the AspNetRoleClaims table
        builder.ToTable("RoleClaims");

        // Primary key
        builder.HasKey(r => r.Id);

        // Index for "normalized" role name to allow efficient lookups
        builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

        // Maps to the AspNetRoles table
        builder.ToTable("Roles");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.Name).HasMaxLength(100);
        builder.Property(u => u.NormalizedName).HasMaxLength(100);

        // The relationships between Role and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each Role can have many entries in the UserRole join table
        builder.HasMany<BaseUserRoleEntity>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany<BaseRoleClaimEntity>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        builder.HasData(
        new BaseRoleEntity
        {
            Id = 1,
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        }
        );
    }
}
