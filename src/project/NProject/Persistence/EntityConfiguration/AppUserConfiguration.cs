using Core.SecurityIdentity.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

        // Maps to the AspNetUsers table
        builder.ToTable("Users");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.UserName).HasMaxLength(50);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
        builder.Property(u => u.Email).HasMaxLength(100);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany<BaseUserClaimEntity>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany<BaseUserLoginEntity>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany<BaseUserTokenEntity>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany<BaseUserRoleEntity>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        builder
           .HasMany(u => u.FavoriteDomains)
           .WithMany(d => d.Users)
           .UsingEntity(j => j.ToTable("AppUserFavoriteDomain"));

        //Owned Entity
        builder.OwnsOne(u => u.RefreshToken, rt =>
        {
            rt.Property(r => r.Token).IsRequired(false);
            rt.Property(r => r.TokenExpires).IsRequired(false);
        });

        var adminUser = new AppUser
        {
            Id = 1,
            UserName = "adminuser",
            NormalizedUserName = "ADMINUSER",
            Email = "adminuser@gmail.com",
            NormalizedEmail = "ADMINUSER@GMAIL.COM",
            PhoneNumber = "+905555555555",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");
        builder.HasData(adminUser);
    }

    //Hashing
    private string CreatePasswordHash(AppUser user, string password)
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        return passwordHasher.HashPassword(user, password);
    }

}
