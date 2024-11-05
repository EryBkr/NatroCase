using MediatR;

namespace Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;

public sealed class DomainUpdatedNotification : INotification
{
    public string Domain { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? LastUpdate { get; set; }

    public DomainUpdatedNotification(string domain, bool isAvailable, DateTime? expiration, DateTime? lastUpdate)
    {
        Domain = domain;
        IsAvailable = isAvailable;
        Expiration = expiration;
        LastUpdate = lastUpdate;
    }
}
