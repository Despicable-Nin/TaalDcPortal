using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Serilog;
using WebApplication2.Data;

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
        try
        {
            foreach (var role in ROLES)
            {
                if (await roleManager.FindByNameAsync(role) == default)
                {
                    Log.Information("Adding {role} to roles.");
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                else
                {
                    Log.Warning($"{role} already exists.");
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            Log.Error(ex.InnerException?.Message);
            Log.Error(ex.StackTrace);

            throw;
        }
    }

    public static async Task CreateUsers(UserManager<IdentityUser> userManager)
    {

        const string ADMIN_ID = "4940CDBE-BAD7-47BB-BEE5-15DDB92939BA";
        const string GUEST_ID = "05F633F6-1674-4322-A347-4A8C6EEFADFE";


        Log.Information("Trying to add new admin user.");
        if (!userManager.Users.Any(u => u.Id == ADMIN))
        {
            var admin = new IdentityUser()
            {
                UserName = "admin@strator.com",
                Email = "admin@strator.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Id = "4940CDBE-BAD7-47BB-BEE5-15DDB92939BA",
            };
            
            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                var createResult = await userManager.CreateAsync(admin, "@dmini$trat0R2023!");

                if (!createResult.Succeeded)
                {
                    Log.Warning(string.Join(Environment.NewLine, createResult.Errors));

                    Log.Warning("Seeding ADMIN identity failed.");
                    return;
                }
            }
            
            Log.Information($"Assigning {ADMIN} role to {admin.UserName} user.");
            var addToRoleResult = await userManager.AddToRoleAsync(admin, Seed.ADMIN);
            
            if (!addToRoleResult.Succeeded)
            {
                Log.Warning(string.Join(Environment.NewLine, addToRoleResult.Errors));
                Log.Information("Adding of new admin user failed.");
            }
        }
        Log.Information("Admin user account has been seeded.");
        Log.Information("==========================================================================");
        Log.Information("Trying to add guest/default user.");

        if (!userManager.Users.Any(u => u.Id == GUEST_ID))
        {
            var guest = new IdentityUser()
            {
                UserName = "guest@taaldc.com",
                Email = "guest@taaldc.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Id = "05F633F6-1674-4322-A347-4A8C6EEFADFE",
            };
            if (await userManager.FindByEmailAsync(guest.Email) == null)
            {
                
                var createResult = await userManager.CreateAsync(guest, "guesT1234!");
                if (!createResult.Succeeded)
                {
                    Log.Warning(string.Join(Environment.NewLine, createResult.Errors));

                    Log.Warning("Seeding GUEST identity failed.");
                    return;
                }
                
                await userManager.AddToRoleAsync(guest, Seed.ADMIN);
                
                Log.Information($"Assigning {ADMIN} role to {guest.UserName} user.");
                var addToRoleResult = await userManager.AddToRoleAsync(guest, Seed.GUEST);
            
                if (!addToRoleResult.Succeeded)
                {
                    Log.Warning(string.Join(Environment.NewLine, addToRoleResult.Errors));
                    Log.Information("Adding of guest / default user failed.");
                }
            }
        }
        Log.Information("Guest / Default user account has been seeded.");
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

                await Seed.CreateRoles(roleManager);

                await Seed.CreateUsers(userManager);

            } 
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Log.Error(ex.InnerException.Message);
                throw;
            }
        }
    }
}