using Core.SecurityIdentity.Encryption;
using Core.SecurityIdentity.Entities;
using Core.SecurityIdentity.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.SecurityIdentity.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    private readonly ILogger<JwtHelper> _logger;

    public JwtHelper(IConfiguration configuration, ILogger<JwtHelper> logger)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _logger = logger;
    }

    public AccessToken CreateToken(BaseUserEntity user)
    {
        _logger.LogInformation($"Token Key: {_tokenOptions.SecurityKey}");
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    public RefreshToken CreateRefreshToken(BaseUserEntity user)
    {
        RefreshToken refreshToken =
            new()
            {
                Token = RandomRefreshToken(),
                TokenExpires = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        BaseUserEntity user,
        SigningCredentials signingCredentials
    )
    {
        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private string RandomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }

    private IEnumerable<Claim> SetClaims(BaseUserEntity user)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        return claims;
    }
}
