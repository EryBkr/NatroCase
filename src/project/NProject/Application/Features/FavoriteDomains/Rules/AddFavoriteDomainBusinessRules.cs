using Application.Features.FavoriteDomains.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FavoriteDomains.Rules;

public sealed class AddFavoriteDomainBusinessRules : BaseBusinessRules
{
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;
    private readonly UserManager<AppUser> _userManager;

    public AddFavoriteDomainBusinessRules(IFavoriteDomainRepository favoriteDomainRepository, UserManager<AppUser> userManager)
    {
        _favoriteDomainRepository = favoriteDomainRepository;
        _userManager = userManager;
    }

    public async Task<FavoriteDomain> FavoriteDomainShouldExist(string domain)
    {
        var favoriteDomain = await _favoriteDomainRepository.GetAsync(
            include: fd => fd.Include(fd => fd.Users),
            predicate: fd => fd.Domain.ToLower() == domain.ToLower()
        );

        if (favoriteDomain == null)
            throw new BusinessException(FavoriteDomainMessages.FavoriteDomainNotFound);

        return favoriteDomain;
    }

    public void UserShouldNotHaveFavoritedDomain(FavoriteDomain favoriteDomain, int? userId)
    {
        bool isAlreadyFavorited = favoriteDomain.Users.Any(u => u.Id == userId);

        if (isAlreadyFavorited)
            throw new BusinessException(FavoriteDomainMessages.DomainAlreadyFavorited);
    }
}
