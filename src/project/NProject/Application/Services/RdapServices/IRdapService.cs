using Domain.Dtos;

namespace Application.Services.RdapServices;

public interface IRdapService
{
    Task<RdapResponse> CheckDomainAvailabilityAsync(string domain);
}
