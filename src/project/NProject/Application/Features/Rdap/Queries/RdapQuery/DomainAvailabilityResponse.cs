namespace Application.Features.Rdap.Queries.RdapQuery;

public record DomainAvailabilityResponse
{
    public string? Handle { get; init; }
    public string? Domain { get; init; }
    public bool IsAvailable { get; init; }
    public DateTime? Expiration { get; init; }
    public DateTime? LastUpdate { get; init; }
}
