using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Data;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}