using Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.FavoriteDomains.Notifications.Common.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DomainUpdatedNotification, FavoriteDomain>()
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.Expiration))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));
    }
}
