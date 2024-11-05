using FluentValidation;

namespace Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenValidator:AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenValidator()
    {
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.RefreshToken).NotEmpty();
    }
}
