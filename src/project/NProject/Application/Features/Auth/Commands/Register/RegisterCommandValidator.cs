using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().EmailAddress().MinimumLength(5);
        RuleFor(c => c.Password).NotEmpty();
        RuleFor(c => c.UserName).NotEmpty();
    }
}
