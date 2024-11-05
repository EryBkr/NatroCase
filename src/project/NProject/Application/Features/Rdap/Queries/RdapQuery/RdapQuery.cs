using MediatR;

namespace Application.Features.Rdap.Queries.RdapQuery;

public sealed record RdapQuery(string Domain) : IRequest<DomainAvailabilityResponse>;
