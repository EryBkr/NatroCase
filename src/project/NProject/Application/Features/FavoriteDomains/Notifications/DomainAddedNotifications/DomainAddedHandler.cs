using Application.Services.Repositories;
using MediatR;

namespace Application.Features.FavoriteDomains.Notifications.DomainAddedNotifications;

public sealed class DomainAddedHandler : INotificationHandler<DomainAddedNotification>
{
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;

    public DomainAddedHandler(IFavoriteDomainRepository favoriteDomainRepository) => _favoriteDomainRepository = favoriteDomainRepository;

    public async Task Handle(DomainAddedNotification notification, CancellationToken cancellationToken)
    {
        var favDomain = notification.NewFavoriteDomain;
        await _favoriteDomainRepository.AddAsync(favDomain, cancellationToken);
    }
}
