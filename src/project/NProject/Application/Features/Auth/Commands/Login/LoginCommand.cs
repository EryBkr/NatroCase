using MediatR;

namespace Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(
        string UserNameOrEmail,
        string Password
    ) : IRequest<LoginCommandResponse>;
