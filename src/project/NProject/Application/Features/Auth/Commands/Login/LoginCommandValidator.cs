using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserNameOrEmail).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
