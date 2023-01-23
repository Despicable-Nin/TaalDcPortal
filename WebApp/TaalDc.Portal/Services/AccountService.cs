using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using TaalDc.Portal.Data;
using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public class AccountService : IAccountService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private ILogger<AccountService> _logger;
    private readonly ApplicationDbContext _applicationDbContext;


    public AccountService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ILogger<AccountService> logger, ApplicationDbContext applicationDbContext)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _logger = logger;
        _applicationDbContext = applicationDbContext;
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
            {
                if (_userManager.IsInRoleAsync(user, r).Result)
                    currentRoles.Add(r);
            }
            var profile =
                _applicationDbContext.UserProfiles.FirstOrDefault(profile => profile.Identity == user.Id);
            vm.AddUser(new UserViewModel(user.Id,user.Email,profile?.FirstName,profile?.LastName,profile?.NameSuffix,profile?.MiddleName,currentRoles.ToArray()));
            
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
    
    public async Task<IEnumerable<string>> GetRolesAsync() => await _roleManager.Roles.AsNoTracking()
        .Where(i => i.NormalizedName != "GUEST")
        .Select(i => i.NormalizedName)
        .OrderBy(i => i)
        .ToArrayAsync();
   
    public async Task<string> CreateUser(CreateUserViewModel vm)
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
        {
            return $"User has been assigned a role already. {user.Email}-{vm.Role}";
        }

        //clear previous roles
        var assignRoleResult = await _userManager.AddToRoleAsync(user, vm.Role);
        if (!assignRoleResult.Succeeded) return string.Join(",", assignRoleResult.Errors);

        _logger.LogInformation("Finished creating identity...");

        _logger.LogInformation("Creating user profile");

        var userProfile = new UserProfile(vm.FirstName, vm.LastName, vm.MiddleName, vm.NameSuffix, user.Id);

        try
        {

            _applicationDbContext.UserProfiles.Add(userProfile);

            await _applicationDbContext.SaveChangesAsync(default);
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(CreateUser), ex);
        }

        
        return string.Empty;

    }

    public async Task<string> UpdateUser(string id, UpdateUserViewModel vm, bool resetPassword = false)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);
        
        //get role
        var current = await _userManager.GetRolesAsync(user);

        var result = await _userManager.RemoveFromRolesAsync(user, current);

        if (!result.Succeeded) return string.Join(",", result.Errors.Select(i => $"{i.Code} - {i.Description}"));
        
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
                profile = new(vm.FirstName, vm.LastName, vm.MiddleName, vm.NameSuffix, user.Id);
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

            await _applicationDbContext.SaveChangesAsync(default);
        }
        catch (Exception ex)
        {
            _logger.LogError(nameof(UpdateUser),new object[]{ex, ex.InnerException});
        }


        return string.Empty;

     }

    public async Task<UserViewModel> GetUserById(string id)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        var roles = await _userManager.GetRolesAsync(user);
        var profile =
            _applicationDbContext.UserProfiles.FirstOrDefault(profile => profile.Identity == user.Id);
        
        return new UserViewModel(user.Id, user.Email, profile?.FirstName, profile?.LastName,profile?.MiddleName,profile?.NameSuffix, roles.ToArray());
    }
}