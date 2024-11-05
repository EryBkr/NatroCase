using FluentValidation;

namespace Application.Features.FavoriteDomains.Commands.AddFavoriteDomainByUserId;

public class AddFavoriteDomainByUserIdCommandValidator : AbstractValidator<AddFavoriteDomainByUserIdCommand>
{
    public AddFavoriteDomainByUserIdCommandValidator()
    {
        RuleFor(i => i.Domain).NotEmpty();
    }
}
