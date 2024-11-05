using FluentValidation;

namespace Application.Features.Rdap.Queries.RdapQuery;

public class RdapQueryValidator : AbstractValidator<RdapQuery>
{
    public RdapQueryValidator()
    {
        RuleFor(query => query.Domain)
            .NotEmpty().WithMessage("Domain cannot be empty.")
            .Matches(@"^(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.[A-Za-z]{2,6}$").WithMessage("Domain format is invalid.")
            .Length(1, 253).WithMessage("Domain length must be between 1 and 253 characters.");
    }
}
