using Application.Features.FavoriteDomains.Constants;
using Application.Features.FavoriteDomains.Rules;
using Application.Services.Repositories;
using Core.SecurityIdentity.AuthenticatedService;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.FavoriteDomains.Commands.DeleteFavoriteDomainByUserId
{
    public sealed class DeleteFavoriteDomainByUserIdCommandHandler : IRequestHandler<DeleteFavoriteDomainByUserIdCommand, DeleteFavoriteDomainByUserIdResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IFavoriteDomainRepository _favoriteDomainRepository;
        private readonly DeleteFavoriteDomainBusinessRules _businessRules;

        public DeleteFavoriteDomainByUserIdCommandHandler(UserManager<AppUser> userManager, IUserService userService, IFavoriteDomainRepository favoriteDomainRepository, DeleteFavoriteDomainBusinessRules businessRules)
        {
            _userManager = userManager;
            _userService = userService;
            _favoriteDomainRepository = favoriteDomainRepository;
            _businessRules = businessRules;
        }

        public async Task<DeleteFavoriteDomainByUserIdResponse> Handle(DeleteFavoriteDomainByUserIdCommand request, CancellationToken cancellationToken)
        {
            var loggInUserId = _userService.GetUserId();

            var favoriteDomain = await _businessRules.FavoriteDomainShouldExistForUser(request.Id, loggInUserId);

            var user = await _userManager.FindByIdAsync(loggInUserId.ToString());

            favoriteDomain.Users.Remove(user);
            await _favoriteDomainRepository.UpdateAsync(favoriteDomain);

            return new DeleteFavoriteDomainByUserIdResponse(FavoriteDomainMessages.DeletedSuccessfully);
        }
    }
}
