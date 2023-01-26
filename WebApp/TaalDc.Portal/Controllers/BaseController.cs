using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaalDc.Portal.Controllers;

[Authorize(Policy = "RequiredRolesAtLeastOne")]
public class BaseController<T> : Controller where T : Controller
{
    protected readonly string Token;
    protected ILogger<T> LoggerInstance;

    public BaseController(ILogger<T> loggerInstance)
    {
        LoggerInstance = loggerInstance;
    }
}