namespace Core.SecurityIdentity.Entities;

public class RefreshToken
{
    public RefreshToken(string refreshToken, DateTime? refreshTokenExpires)
    {
        Token = refreshToken;
        TokenExpires = refreshTokenExpires;
    }

    public RefreshToken()
    {
        Token = string.Empty;
        TokenExpires = DateTime.MinValue;
    }

    public string Token { get; set; }
    public DateTime? TokenExpires { get; set; }
}
