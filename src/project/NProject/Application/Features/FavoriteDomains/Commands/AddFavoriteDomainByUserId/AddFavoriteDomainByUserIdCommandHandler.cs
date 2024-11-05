using Application.Features.FavoriteDomains.Constants;
using Application.Features.FavoriteDomains.Rules;
using Application.Services.Repositories;
using Core.SecurityIdentity.AuthenticatedService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FavoriteDomains.Commands.AddFavoriteDomainByUserId
{
    public sealed class AddFavoriteDomainByUserIdCommandHandler : IRequestHandler<AddFavoriteDomainByUserIdCommand, AddFavoriteDomainByUserIdResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IFavoriteDomainRepository _favoriteDomainRepository;
        private readonly AddFavoriteDomainBusinessRules _businessRules;

        public AddFavoriteDomainByUserIdCommandHandler(UserManager<AppUser> userManager, IUserService userService, IFavoriteDomainRepository favoriteDomainRepository, AddFavoriteDomainBusinessRules businessRules)
        {
            _userManager = userManager;
            _userService = userService;
            _favoriteDomainRepository = favoriteDomainRepository;
            _businessRules = businessRules;
        }

        public async Task<AddFavoriteDomainByUserIdResponse> Handle(AddFavoriteDomainByUserIdCommand request, CancellationToken cancellationToken)
        {
            var loggInUserId = _userService.GetUserId();

            var favoriteDomain = await _businessRules.FavoriteDomainShouldExist(request.Domain);
            _businessRules.UserShouldNotHaveFavoritedDomain(favoriteDomain, loggInUserId);

            var user = await _userManager.FindByIdAsync(loggInUserId.ToString());

            favoriteDomain.Users.Add(user);
            await _favoriteDomainRepository.UpdateAsync(favoriteDomain);

            return await Task.FromResult(new AddFavoriteDomainByUserIdResponse(favoriteDomain.Id));
        }
    }
}
