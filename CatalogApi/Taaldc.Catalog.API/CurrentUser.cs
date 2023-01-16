using System.Security.Claims;
using SeedWork;

namespace Taaldc.Catalog.API;

public class CurrentUser : IAmCurrentUser
{
    private readonly bool _isAuthenticated;


    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        IdentityId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid);
        Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(i => i.Value).ToArray();
        _isAuthenticated = !string.IsNullOrEmpty(Email);
    }
    
    public string[] Roles { get; }
    public string Email { get; }
    public string IdentityId { get; }
}