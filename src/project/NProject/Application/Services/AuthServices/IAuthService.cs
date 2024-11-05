using Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;
using Application.Features.Auth.Commands.Login;
using Domain.Entities;

namespace Application.Services.AuthServices;

public interface IAuthService
{
    Task RegisterAsync(AppUser user, string password);
    Task<LoginCommandResponse> LoginAsync(LoginCommand loginCommand, CancellationToken cancellationToken);
    Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand command, CancellationToken cancellationToken);
}
