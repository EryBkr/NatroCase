namespace Core.SecurityIdentity.JWT;

public sealed class TokenOptions
{
    public int AccessTokenExpiration { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int RefreshTokenTTL { get; set; }
    public string SecurityKey { get; set; }

    public TokenOptions()
    {
        Audience = string.Empty;
        Issuer = string.Empty;
        SecurityKey = string.Empty;
    }

    public TokenOptions(int accessTokenExpiration, string audience, string issuer, int refreshTokenTtl, string securityKey)
    {
        AccessTokenExpiration = accessTokenExpiration;
        Audience = audience;
        Issuer = issuer;
        RefreshTokenTTL = refreshTokenTtl;
        SecurityKey = securityKey;
    }

}