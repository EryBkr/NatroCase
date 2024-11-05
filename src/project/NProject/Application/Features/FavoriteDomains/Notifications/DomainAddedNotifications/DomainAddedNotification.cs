using Domain.Entities;
using MediatR;

namespace Application.Features.FavoriteDomains.Notifications.DomainAddedNotifications;

public sealed class DomainAddedNotification : INotification
{
    public FavoriteDomain NewFavoriteDomain { get; set; }

    public DomainAddedNotification(FavoriteDomain newFavoriteDomain) => NewFavoriteDomain = newFavoriteDomain;
}
