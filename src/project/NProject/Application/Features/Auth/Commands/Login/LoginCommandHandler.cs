using Application.Services.AuthServices;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private IAuthService _authService;

    public LoginCommandHandler(IAuthService authService) => _authService = authService;

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken) => await _authService.LoginAsync(request, cancellationToken);
}
