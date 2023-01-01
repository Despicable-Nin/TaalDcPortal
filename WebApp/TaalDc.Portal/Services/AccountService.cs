using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public class AccountService : IAccountService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private ILogger<AccountService> _logger;


    public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
        ILogger<AccountService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<UserIndexViewModel> GetListOfUsersWithRoles()
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
            vm.AddUser(new UserViewModel(i.Email, currentRoles.ToArray()));
        });

        return vm;
    }

    public async Task<IEnumerable<BrokerListsViewModel>> GetBrokers()
    {
        var brokers = await _userManager.GetUsersInRoleAsync(Seed.Seed.BROKER);

        //to list so that it doesn't end up null like array
        var vm = brokers.Select(i => new BrokerListsViewModel(i.NormalizedUserName, i.NormalizedEmail, i.Id)).ToList();

        return vm.ToArray();
    }
}