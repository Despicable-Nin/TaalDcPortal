using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.Controllers;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Users;

namespace WebApplication2.Controllers;

[Authorize(Policy = "AdminOnly")]
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
        var vm = await _accountService.GetListOfUsers();
        return View(vm);
    }

    public async Task<IActionResult> CreateUser()
    {
        //get dropdown of roles
        ViewData["Roles"] = await _accountService.GetRolesAsync();

        return View();
    }


    [HttpPost("post")]
    public async Task<IActionResult> Post(UserCreateDto model)
    {
        var result = await _accountService.CreateUser(model);

        if (string.IsNullOrEmpty(result)) return RedirectToAction(nameof(Index));

        //get dropdown of roles
        ViewData["Roles"] = await _accountService.GetRolesAsync();
        ViewData["Errors"] = result;
        return RedirectToAction(nameof(CreateUser));
    }

    public async Task<IActionResult> UpdateUser(string id)
    {
        var user = await _accountService.GetUserById(id);
        //get dropdown of roles
        ViewData["Roles"] = await _accountService.GetRolesAsync();

        UserUpdateDto model = new()
        {
            Id = id,
            Emailaddress = user.Email.Normalize(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            NameSuffix = user.NameSuffix,
            Company = user.Company,
            PRCLicense = user.PRCLicense,
            Role = user.Roles.FirstOrDefault() ?? string.Empty,
            IsActive = user.Roles.Any(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Put(string id, UserUpdateDto model)
    {
        var result = await _accountService.UpdateUser(model.Id, model, false);

        if (string.IsNullOrEmpty(result)) return RedirectToAction(nameof(Index));

        ViewData["Roles"] = await _accountService.GetRolesAsync();

        return RedirectToAction(nameof(UpdateUser), model);
    }


}