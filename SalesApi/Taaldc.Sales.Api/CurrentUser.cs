using System.Security.Claims;
using SeedWork;

namespace Taaldc.Sales.Api;


public class CurrentUser : IAmCurrentUser
{
    private readonly bool _isAuthenticated;


    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        IdentityId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(i => i.Value).ToArray();
        _isAuthenticated = !string.IsNullOrEmpty(Email);
    }
    
    public string Name { get; }
    public string[] Roles { get; }
    public string Email { get; }
    public string IdentityId { get; }
    
    
    public bool IsAdmin() => Roles.Contains("ADMIN");
    public bool IsBroker() => Roles.Contains("BROKER");
}