using Core.SecurityIdentity.Entities;

namespace Domain.Entities;

public sealed class AppUser : BaseUserEntity
{
    public ICollection<FavoriteDomain> FavoriteDomains { get; set; }

    public AppUser() => FavoriteDomains = new HashSet<FavoriteDomain>();
}
