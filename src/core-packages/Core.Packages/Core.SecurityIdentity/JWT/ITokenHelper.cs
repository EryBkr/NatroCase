using Core.SecurityIdentity.Entities;

namespace Core.SecurityIdentity.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(BaseUserEntity user);
    RefreshToken CreateRefreshToken(BaseUserEntity user);
}

