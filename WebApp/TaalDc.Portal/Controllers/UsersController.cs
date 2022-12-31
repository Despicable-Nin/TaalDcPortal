using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using TaalDc.Portal.Controllers;
using TaalDc.Portal.Services;

namespace WebApplication2.Controllers;

[Authorize]
public class UsersController : BaseController<UsersController>
{
    private readonly IAccountService _accountService;


    public UsersController( ILogger<UsersController> loggerInstance,  IAccountService accountService) : base(loggerInstance)
    {
        _accountService = accountService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = await _accountService.GetListOfUsersWithRoles();
        return View(vm);
    }
    
}
