using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using TaalDc.Portal.Controllers;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Users;

namespace WebApplication2.Controllers;

[Authorize]
public class UsersController : BaseController<UsersController>
{
    private readonly IAccountService _accountService;
   
    public UsersController(
        ILogger<UsersController> loggerInstance,  
        IAccountService accountService) : base(loggerInstance)
    {
        _accountService = accountService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = await _accountService.GetListOfUsersWithRoles();
        return View(vm);
    }
    
    public async Task<IActionResult> CreateUser()
    {

        //get dropdown of roles
        ViewData["Roles"] = await _accountService.GetRolesAsync();
           
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Post(CreateUserViewModel model)
    {
        var result = await _accountService.CreateUser(model);

        if (string.IsNullOrEmpty(result)) return RedirectToAction(nameof(Index));

        //get dropdown of roles
        ViewData["Roles"] = await _accountService.GetRolesAsync();
        ViewData["Errors"] = result;
        return Redirect("");
    }
    
}
