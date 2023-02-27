using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TaalDc.Portal.Data;
using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<AccountService> _logger;
    private readonly IConfiguration _configuration;


    public AccountService(
        RoleManager<IdentityRole> roleManager, 
        UserManager<IdentityUser> userManager,
        ILogger<AccountService> logger, 
        ApplicationDbContext applicationDbContext,
        IConfiguration configuration
        )
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _logger = logger;
        _applicationDbContext = applicationDbContext;
        _configuration = configuration;
    }
    
      public async Task<string> GetToken(string email)
        {
   
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.NormalizedEmail),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.NormalizedUserName)
                };

                foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                
                var token = new JwtSecurityToken(
                    _configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                //TODO: we can try storing this to Session or Cookie after..
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, ex.InnerException?.Message);
            }

        return string.Empty;
    }

    public async Task<UserIndexViewModel> GetListOfUsers()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();
        var roles = await _roleManager.Roles.AsNoTracking().ToArrayAsync();

        UserIndexViewModel vm = new();

        foreach (var user in users)
        {
            var currentRoles = new List<string>();
            foreach (var r in roles.Select(ro => ro.NormalizedName))
                if (_userManager.IsInRoleAsync(user, r).Result)
                    currentRoles.Add(r);
            var profile =
                _applicationDbContext.UserProfiles.FirstOrDefault(profile => profile.Identity == user.Id);
            vm.AddUser(new UserViewModel(user.Id, user.Email, profile?.FirstName, profile?.LastName,
                profile?.NameSuffix, profile?.MiddleName, currentRoles.ToArray(), currentRoles.Any()));
        }

        return vm;
    }

    public async Task<IEnumerable<BrokerListsViewModel>> GetBrokers()
    {
        var brokers = await _userManager.GetUsersInRoleAsync(Seed.Seed.BROKER);

        //to list so that it doesn't end up null like array
        var vm = brokers.Select(i => new BrokerListsViewModel(i.NormalizedUserName, i.NormalizedEmail, i.Id)).ToList();

        //we can try to store this in localstorage
        return vm.ToArray();
    }

    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        return await _roleManager.Roles.AsNoTracking()
            .Where(i => i.NormalizedName != "GUEST")
            .Select(i => i.NormalizedName)
            .OrderBy(i => i)
            .ToArrayAsync();
    }

    public async Task<string> CreateUser(UserCreateDto vm)
    {
        var user = await _userManager.FindByEmailAsync(vm.Emailaddress);

        if (user == default)
        {
            user = new IdentityUser
            {
                UserName = vm.Emailaddress,
                Email = vm.Emailaddress,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Id = Guid.NewGuid().ToString().ToUpper()
            };

            var createResult = await _userManager.CreateAsync(user, vm.DefaultPassword);

            if (!createResult.Succeeded)
            {
                _logger.LogWarning(string.Join(Environment.NewLine, createResult.Errors));
                return string.Join(Environment.NewLine, createResult.Errors);
            }
        }

        //if successfully created or fetched -- try to check if user has a role, if not, assign role
        if (await _userManager.IsInRoleAsync(user, vm.Role))
            return $"User has been assigned a role already. {user.Email}-{vm.Role}";

        //clear previous roles
        var assignRoleResult = await _userManager.AddToRoleAsync(user, vm.Role);
        if (!assignRoleResult.Succeeded) return string.Join(",", assignRoleResult.Errors);

        _logger.LogInformation("Finished creating identity...");

        _logger.LogInformation("Creating user profile");

        var userProfile = new UserProfile(vm.FirstName, vm.LastName, vm.MiddleName, vm.NameSuffix, user.Id);

        try
        {
            _applicationDbContext.UserProfiles.Add(userProfile);

            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(CreateUser), ex);
        }


        return string.Empty;
    }

    public async Task<string> UpdateUser(string id, UserUpdateDto vm, bool resetPassword = false)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);

        //get role
        var current = await _userManager.GetRolesAsync(user);

        var result = await _userManager.RemoveFromRolesAsync(user, current);

        if (!result.Succeeded) return string.Join(",", result.Errors.Select(i => $"{i.Code} - {i.Description}"));

        if (vm.IsActive)
            result = await _userManager.AddToRoleAsync(user, vm.Role);

        if (!result.Succeeded) return string.Join(",", result.Errors.Select(i => $"{i.Code} - {i.Description}"));

        if (resetPassword)
        {
            result = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
            if (!result.Succeeded) return string.Join(",", result.Errors.Select(i => $"{i.Code} - {i.Description}"));
        }

        try
        {
            var profile =
                _applicationDbContext.UserProfiles.FirstOrDefault(profile => profile.Identity == user.Id);

            if (profile == default)
            {
                profile = new UserProfile(vm.FirstName, vm.LastName, vm.MiddleName, vm.NameSuffix, user.Id);
                _applicationDbContext.UserProfiles.Add(profile);
            }
            else
            {
                profile.FirstName = vm.FirstName;
                profile.MiddleName = vm.MiddleName;
                profile.LastName = vm.LastName;
                profile.NameSuffix = vm.NameSuffix;

                _applicationDbContext.UserProfiles.Update(profile);
            }

            await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(UpdateUser), ex, ex.InnerException);
        }


        return string.Empty;
    }

    public async Task<UserViewModel> GetUserById(string id)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        var roles = await _userManager.GetRolesAsync(user);
        var profile =
            _applicationDbContext.UserProfiles.FirstOrDefault(profile => profile.Identity == user.Id);

        return new UserViewModel(user.Id, user.Email, profile?.FirstName, profile?.LastName, profile?.MiddleName,
            profile?.NameSuffix, roles.ToArray(), roles.Any());
    }
}