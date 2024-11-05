namespace Application.Features.Auth.Commands.Login;

public sealed record LoginCommandResponse(
        string Token,
        string RefreshToken,
        DateTime? RefreshTokenExpires,
        int UserId,
        string? Email
    );