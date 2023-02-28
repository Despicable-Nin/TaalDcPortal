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
        
        var data = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.UserData);
        if (data != default)
        {
            _company = data.Split("-")[0];
            _prcLicense = data.Split("-")[1];
        }
    }
    
    private readonly string _company;
    private readonly string _prcLicense;
    public string GetCompany() => _company;
    public string GetPrcLicense() => _prcLicense;
    public string Name { get; }
    public string[] Roles { get; }
    public string Email { get; }
    public string IdentityId { get; }


    public bool IsAdmin()
    {
        return Roles.Contains("ADMIN");
    }

    public bool IsBroker()
    {
        return Roles.Contains("BROKER");
    }
}