using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.SecurityIdentity.AuthenticatedService;

public sealed class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public int? GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity is not { IsAuthenticated: true })
            return null;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");

        return userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : (int?)null;
    }
}
