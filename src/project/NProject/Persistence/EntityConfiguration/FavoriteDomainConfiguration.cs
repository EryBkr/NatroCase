using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class FavoriteDomainConfiguration : IEntityTypeConfiguration<FavoriteDomain>
{
    public void Configure(EntityTypeBuilder<FavoriteDomain> builder)
    {
        builder.ToTable(nameof(FavoriteDomain) + "s").HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("Id").HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();

        builder.Property(b => b.Domain).HasColumnName("Domain").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(b => b.Users);

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);

        builder.HasIndex(b => b.Domain, name: "UK_FavoriteDomains_Domain").IsUnique();
    }
}
