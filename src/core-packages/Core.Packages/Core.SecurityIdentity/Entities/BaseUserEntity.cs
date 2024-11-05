using Microsoft.AspNetCore.Identity;

namespace Core.SecurityIdentity.Entities;

public class BaseUserEntity : IdentityUser<int>
{
    //Owned Entity
    public RefreshToken? RefreshToken { get; set; }
}
