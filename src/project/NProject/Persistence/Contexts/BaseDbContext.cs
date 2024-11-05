using Core.SecurityIdentity.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : IdentityDbContext<AppUser, BaseRoleEntity, int, BaseUserClaimEntity, BaseUserRoleEntity, BaseUserLoginEntity, BaseRoleClaimEntity, BaseUserTokenEntity>
{
    protected IConfiguration Configuration { get; set; }

    public DbSet<FavoriteDomain> FavoriteDomains { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
