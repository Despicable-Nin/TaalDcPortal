using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.Services;

namespace WebApplication2.Controllers;

public class UsersController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = await _accountService.GetListOfUsersWithRoles();
        return View(vm);
    }
}
