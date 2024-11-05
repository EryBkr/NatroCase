using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterCommand, AppUser>();
    }
}
