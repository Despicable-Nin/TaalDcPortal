using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Seed;

public static class Seed
{
    public const string ADMIN = nameof(ADMIN);
    public const string MARKETING = nameof(MARKETING);
    public const string SALES = nameof(SALES);
    public const string BROKER = nameof(BROKER);
    public const string GUEST = nameof(GUEST);

    public static string[] ROLES = new[]
    {
        ADMIN,
        MARKETING,
        SALES,
        BROKER,
        GUEST
    };

    public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        ROLES.ToList().ForEach(async r => await roleManager.CreateAsync(new IdentityRole(r)));
    }

    public static async Task Initialize(WebApplication app)
    {
        
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                Seed.CreateRoles(roleManager);
        
                var defaultUser = new IdentityUser
                {
                    UserName = "admin@strator.com",
                    Email = "admin@strator.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
        
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "admin1234!");
                        await userManager.AddToRoleAsync(defaultUser, Seed.ADMIN);
                    }
                }
            } 
            catch (Exception)
            {
                throw;
            }
        }
    }
}