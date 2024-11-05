using Application.Features.FavoriteDomains.Notifications.DomainAddedNotifications;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;

public sealed class DomainUpdatedHandler : INotificationHandler<DomainUpdatedNotification>
{
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DomainUpdatedHandler(IFavoriteDomainRepository favoriteDomainRepository, IMediator mediator, IMapper mapper)
    {
        _favoriteDomainRepository = favoriteDomainRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Handle(DomainUpdatedNotification notification, CancellationToken cancellationToken)
    {
        var existingFavorite = await _favoriteDomainRepository.GetAsync(
                       predicate: b => b.Domain.ToLower() == notification.Domain.ToLower(),
                       cancellationToken: cancellationToken);

        if (existingFavorite != null)
        {
            existingFavorite.IsAvailable = notification.IsAvailable;
            existingFavorite.ExpirationDate = notification.Expiration;
            existingFavorite.LastUpdate = notification.LastUpdate;

            await _favoriteDomainRepository.UpdateAsync(existingFavorite, cancellationToken);
        }
        else
        {
            var favDomain = _mapper.Map<FavoriteDomain>(notification);
            await _mediator.Publish(new DomainAddedNotification(favDomain));
        }
            
    }
}
