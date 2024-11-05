using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Response;
using Core.Persistence.Paging;
using Core.SecurityIdentity.AuthenticatedService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FavoriteDomains.Queries;

public sealed class GetListByDynamicFavoriteDomainQueryHandler : IRequestHandler<GetListByDynamicFavoriteDomainQuery, GetListResponse<FavoriteDomainListItemDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;
    private readonly IMapper _mapper;

    public GetListByDynamicFavoriteDomainQueryHandler(UserManager<AppUser> userManager, IUserService userService, IFavoriteDomainRepository favoriteDomainRepository, IMapper mapper)
    {
        _userManager = userManager;
        _userService = userService;
        _favoriteDomainRepository = favoriteDomainRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<FavoriteDomainListItemDto>> Handle(GetListByDynamicFavoriteDomainQuery request, CancellationToken cancellationToken)
    {
        var loggInUserId = _userService.GetUserId();

        Paginate<FavoriteDomain> domains = await _favoriteDomainRepository.GetListByDynamicAsync(
                   dynamic: request.DynamicQuery,
                   include: m => m.Include(m => m.Users),
                   predicate: m => m.Users.Any(m => m.Id == loggInUserId),
                   index: request.PageRequest.PageIndex,
                   size: request.PageRequest.PageSize
               );

        var response = _mapper.Map<GetListResponse<FavoriteDomainListItemDto>>(domains);
        return response;
    }
}
