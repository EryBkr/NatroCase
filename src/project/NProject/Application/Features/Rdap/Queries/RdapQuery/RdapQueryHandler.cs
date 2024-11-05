using Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;
using Application.Services.RdapServices;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Rdap.Queries.RdapQuery;

public sealed class RdapQueryHandler : IRequestHandler<RdapQuery, DomainAvailabilityResponse>
{
    private readonly IRdapService _rdapService;
    private readonly IMapper _mapper;
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;
    private readonly IMediator _mediator;


    public RdapQueryHandler(IRdapService rdapService, IMapper mapper, IFavoriteDomainRepository favoriteDomainRepository, IMediator mediator)
    {
        _rdapService = rdapService;
        _mapper = mapper;
        _favoriteDomainRepository = favoriteDomainRepository;
        _mediator = mediator;
    }

    public async Task<DomainAvailabilityResponse> Handle(RdapQuery request, CancellationToken cancellationToken)
    {
        var rdapResponse = await _rdapService.CheckDomainAvailabilityAsync(request.Domain);
        var domainAvailability = rdapResponse == null
                         ? new DomainAvailabilityResponse
                         {
                             Handle = null,
                             Domain = request.Domain,
                             IsAvailable = true,
                             Expiration = null,
                             LastUpdate = null
                         }
                         : _mapper.Map<DomainAvailabilityResponse>(rdapResponse);


        var notification = _mapper.Map<DomainUpdatedNotification>(domainAvailability);
        await _mediator.Publish(notification);

        return domainAvailability;
    }
}
