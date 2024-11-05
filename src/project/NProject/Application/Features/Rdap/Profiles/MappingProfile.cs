using Application.Features.Rdap.Queries.RdapQuery;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Features.Rdap.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RdapResponse, DomainAvailabilityResponse>()
             .ConvertUsing(src => src == null
                 ? new DomainAvailabilityResponse
                 {
                     Handle = null,
                     Domain = null,
                     IsAvailable = true,
                     Expiration = null,
                     LastUpdate = null
                 }
                 : new DomainAvailabilityResponse
                 {
                     Handle = src.Handle,
                     Domain=src.LdhName,
                     IsAvailable = src.Events == null || !src.Events.Any(),
                     Expiration = GetExpirationDate(src),
                     LastUpdate = GetLastChangedDate(src)
                 });

        CreateMap<DomainAvailabilityResponse, FavoriteDomain>()
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.Expiration))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
            .ForMember(dest => dest.Users, opt => opt.Ignore());
    }


    private DateTime? GetExpirationDate(RdapResponse src)
    {
        if (src?.Events == null) return null;
        var expirationEvent = src.Events.FirstOrDefault(e => e.EventAction == "expiration");
        return expirationEvent?.EventDate;
    }

    private DateTime? GetLastChangedDate(RdapResponse src)
    {
        if (src?.Events == null) return null;
        var lastChangedEvent = src.Events.FirstOrDefault(e => e.EventAction == "last update of RDAP database");
        return lastChangedEvent?.EventDate;
    }
}
