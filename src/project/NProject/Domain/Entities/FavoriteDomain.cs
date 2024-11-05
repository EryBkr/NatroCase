using Core.Persistence.Repositories;

namespace Domain.Entities;

public class FavoriteDomain : Entity<Guid>
{
    public string Domain { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? LastUpdate { get; set; }
    public bool IsAvailable { get; set; }


    public virtual ICollection<AppUser> Users { get; set; }

    public FavoriteDomain() => Users = new HashSet<AppUser>();
    public FavoriteDomain(string domain, DateTime expirationDate, bool ısAvailable) : this()
    {
        Domain = domain;
        ExpirationDate = expirationDate;
        IsAvailable = ısAvailable;
    }
}
