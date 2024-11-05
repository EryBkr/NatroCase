using Core.Application.Requests;
using Core.Application.Response;
using Core.Persistence.Dynamic;
using MediatR;

namespace Application.Features.FavoriteDomains.Queries;

public sealed class GetListByDynamicFavoriteDomainQuery: IRequest<GetListResponse<FavoriteDomainListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
}
