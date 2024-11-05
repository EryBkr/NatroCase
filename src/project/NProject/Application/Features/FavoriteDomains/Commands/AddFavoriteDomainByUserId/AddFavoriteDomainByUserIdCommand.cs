using MediatR;

namespace Application.Features.FavoriteDomains.Commands.AddFavoriteDomainByUserId;

public sealed record AddFavoriteDomainByUserIdCommand(string Domain) : IRequest<AddFavoriteDomainByUserIdResponse>;
