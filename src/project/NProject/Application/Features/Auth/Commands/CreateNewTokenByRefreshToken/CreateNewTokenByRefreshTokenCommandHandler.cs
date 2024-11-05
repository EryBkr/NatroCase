using Application.Features.Auth.Commands.Login;
using Application.Services.AuthServices;
using MediatR;

namespace Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenCommandHandler : IRequestHandler<CreateNewTokenByRefreshTokenCommand, LoginCommandResponse>
{
    private readonly IAuthService _authService;

    public CreateNewTokenByRefreshTokenCommandHandler(IAuthService authService) => _authService = authService;

    public async Task<LoginCommandResponse> Handle(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.CreateTokenByRefreshTokenAsync(request, cancellationToken);
        return response;
    }
}
