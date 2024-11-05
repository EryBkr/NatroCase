using Application.Features.FavoriteDomains.Notifications.DomainUpdatedNotifications;
using Application.Features.FavoriteDomains.Queries;
using Application.Features.Rdap.Queries.RdapQuery;
using AutoMapper;
using Core.Application.Response;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.FavoriteDomains.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FavoriteDomain, FavoriteDomainListItemDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
           .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
           .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
           .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
           .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate));

        CreateMap<DomainAvailabilityResponse, DomainUpdatedNotification>()
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
            .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Expiration))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate));

        CreateMap(typeof(Paginate<>), typeof(GetListResponse<>))
           .ForMember("Items", opt => opt.MapFrom("Items"))
           .ForMember("Size", opt => opt.MapFrom("Size"))
           .ForMember("Index", opt => opt.MapFrom("Index"))
           .ForMember("Count", opt => opt.MapFrom("Count"))
           .ForMember("Pages", opt => opt.MapFrom("Pages"))
           .ForMember("HasPrevious", opt => opt.MapFrom("HasPrevious"))
           .ForMember("HasNext", opt => opt.MapFrom("HasNext"));
    }
}
