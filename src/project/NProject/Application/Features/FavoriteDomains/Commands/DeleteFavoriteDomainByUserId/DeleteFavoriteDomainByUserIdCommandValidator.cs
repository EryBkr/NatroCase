using FluentValidation;

namespace Application.Features.FavoriteDomains.Commands.DeleteFavoriteDomainByUserId;

public class DeleteFavoriteDomainByUserIdCommandValidator : AbstractValidator<DeleteFavoriteDomainByUserIdCommand>
{
    public DeleteFavoriteDomainByUserIdCommandValidator()
    {
        RuleFor(i => i.Id).NotNull().NotEmpty();
    }
}

