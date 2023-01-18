using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public class AccountService : IAccountService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly SignInManager<IdentityUser> _signInManager;
    private ILogger<AccountService> _logger;


    public AccountService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IEmailSender emailSender, SignInManager<IdentityUser> signInManager,  ILogger<AccountService> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _emailSender = emailSender;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<UserIndexViewModel> GetListOfUsers()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();
        var roles = await _roleManager.Roles.AsNoTracking().ToArrayAsync();

        UserIndexViewModel vm = new();

        users.ForEach(i =>
        {
            var currentRoles = new List<string>();
            foreach (var r in roles)
                if (_userManager.IsInRoleAsync(i, r.Name).Result)
                    currentRoles.Add(r.Name);
            vm.AddUser(new UserViewModel(i.NormalizedUserName, i.NormalizedEmail, i.Id, currentRoles.ToArray()));
        });

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
    
    public async Task<IEnumerable<string>> GetRolesAsync() =>await _roleManager.Roles.Select(i => i.NormalizedName).ToArrayAsync();
   
    public async Task<string> CreateUser(CreateUserViewModel vm)
    {
        
        var user = await _userManager.FindByEmailAsync(vm.Emailaddress);

        if (user == default)
        {
            user = new IdentityUser
            {
                UserName = vm.Username,
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

        _logger.LogInformation("Finished seeding identity...");

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

        user.UserName = vm.Username;

        result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return string.Join(",", result.Errors.Select(i => $"{i.Code} - {i.Description}"));


        return string.Empty;

    }

    public async Task<UserViewModel> GetUserById(string id)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        var roles = await _userManager.GetRolesAsync(user);
        return new UserViewModel(user.UserName, user.Email, user.Id, roles.ToArray());
    }
}