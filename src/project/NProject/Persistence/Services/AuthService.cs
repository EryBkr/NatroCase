using Application.Features.Auth.Commands.CreateNewTokenByRefreshToken;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Constant;
using Application.Services.AuthServices;
using Core.SecurityIdentity.Entities;
using Core.SecurityIdentity.JWT;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHelper _tokenHelper;

    public AuthService(UserManager<AppUser> userManager, ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _tokenHelper = tokenHelper;
    }

    public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByIdAsync(command.UserId.ToString());

        if (user == null)
            throw new Exception(AuthMessages.UserNotFound);

        if (user.RefreshToken.Token != command.RefreshToken)
            throw new Exception(AuthMessages.InvalidRefreshToken);

        if (user.RefreshToken.TokenExpires < DateTime.UtcNow)
            throw new Exception(AuthMessages.ExpireRefreshToken);

        return await CreateToken(user);
    }

    public async Task<LoginCommandResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _userManager.Users
            .Where(u =>
            u.UserName == command.UserNameOrEmail ||
            u.Email == command.UserNameOrEmail).FirstOrDefaultAsync(cancellationToken);

        if (appUser == null)
            throw new Exception(AuthMessages.UserNotFound);

        var result = await _userManager.CheckPasswordAsync(appUser, command.Password);

        if (!result)
            throw new Exception(AuthMessages.LoginAttempt);

        return await CreateToken(appUser);
    }

    public async Task RegisterAsync(AppUser command, string password)
    {
        IdentityResult result = await _userManager.CreateAsync(command, password);

        if (!result.Succeeded)
            throw new Exception(result.Errors?.First()?.Description);
    }


    private async Task<LoginCommandResponse> CreateToken(AppUser user)
    {
        var accessToken = _tokenHelper.CreateToken(user);
        var refreshToken = _tokenHelper.CreateRefreshToken(user);

        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        return new LoginCommandResponse(
            Token: accessToken.Token,
            RefreshToken: refreshToken.Token,
            RefreshTokenExpires: refreshToken.TokenExpires,
            Email: user.Email,
            UserId: user.Id);
    }
}
