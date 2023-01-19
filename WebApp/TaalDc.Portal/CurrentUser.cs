using System.Security.Claims;
using SeedWork;

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
}