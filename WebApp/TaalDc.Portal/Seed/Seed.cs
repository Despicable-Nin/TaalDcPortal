using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace TaalDc.Portal.Seed;

public class Seed
{
    public const string ADMIN = nameof(ADMIN);
    public const string MARKETING = nameof(MARKETING);
    public const string SALES = nameof(SALES);
    public const string BROKER = nameof(BROKER);
    public const string STAFF = nameof(STAFF);

    public static string[] ROLES =
    {
        ADMIN,
        MARKETING,
        SALES,
        BROKER,
        STAFF
    };

    public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        try
        {
            foreach (var role in ROLES)
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
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            Log.Error(ex.InnerException?.Message);
            Log.Error(ex.StackTrace);

            throw;
        }
    }


    public static async Task CreateUsers(UserManager<IdentityUser> userManager, string role, string id, string email,
        string password)
    {
        Log.Information($"Start seeding user with {role} role.");

        //check if ID exists
        Log.Information($"Checking if admin with id:{id} exists...");
        var user = await userManager.FindByEmailAsync(email);

        if (user == default)
        {
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Id = id
            };

            var createResult = await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                Log.Warning(string.Join(Environment.NewLine, createResult.Errors));
                Log.Warning($"Seeding {role} identity failed.");

                return;
            }
        }

        //if successfully created or fetched -- try to check if user has a role, if not, assign role
        if (await userManager.IsInRoleAsync(user, role))
        {
            Log.Information($"User has been assigned a role already. {user.Email}-{role}");
            return;
        }

        var assignRoleResult = await userManager.AddToRoleAsync(user, role);
        if (!assignRoleResult.Succeeded) Log.Warning(string.Join(",", assignRoleResult.Errors));

        Log.Information("Finished seeding identity...");
    }

    public static async Task Initialize(WebApplication app)
    {
        const string ADMIN_ID = "4940CDBE-BAD7-47BB-BEE5-15DDB92939BA";

        const string ADMIN_EMAIL = "admin@strator.com";

        const string ADMIN_PASSWORD = "@dmini$trat0R2023!";

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await CreateRoles(roleManager);

                await CreateUsers(userManager, ADMIN, ADMIN_ID, ADMIN_EMAIL, ADMIN_PASSWORD);
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