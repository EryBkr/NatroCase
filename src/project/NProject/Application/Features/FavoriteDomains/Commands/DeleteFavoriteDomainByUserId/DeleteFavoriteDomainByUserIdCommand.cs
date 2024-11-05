using MediatR;

namespace Application.Features.FavoriteDomains.Commands.DeleteFavoriteDomainByUserId;

public sealed record DeleteFavoriteDomainByUserIdCommand(Guid Id) : IRequest<DeleteFavoriteDomainByUserIdResponse>;

