using Application.Features.FavoriteDomains.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FavoriteDomains.Rules;

public sealed class DeleteFavoriteDomainBusinessRules : BaseBusinessRules
{
    private readonly IFavoriteDomainRepository _favoriteDomainRepository;

    public DeleteFavoriteDomainBusinessRules(IFavoriteDomainRepository favoriteDomainRepository) => _favoriteDomainRepository = favoriteDomainRepository;

    public async Task<FavoriteDomain> FavoriteDomainShouldExistForUser(Guid domainId, int? userId)
    {
        var favoriteDomain = await _favoriteDomainRepository.GetAsync(
            include: fd => fd.Include(fd => fd.Users),
            predicate: fd => fd.Id == domainId && fd.Users.Any(u => u.Id == userId)
        );

        if (favoriteDomain == null)
            throw new BusinessException(FavoriteDomainMessages.FavoriteDomainNotFound);

        return favoriteDomain;
    }
}
