using Application.Features.Auth.Commands.Login;
using MediatR;

namespace Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;

public sealed record CreateNewTokenByRefreshTokenCommand(
        int UserId,
        string RefreshToken
    ):IRequest<LoginCommandResponse>;
