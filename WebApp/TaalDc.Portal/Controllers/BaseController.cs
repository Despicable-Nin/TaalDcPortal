using Microsoft.AspNetCore.Mvc;
using SeedWork;

namespace TaalDc.Portal.Controllers;


public class BaseController<T> : Controller where T : Controller
{
    protected readonly string Token;
    protected ILogger<T> LoggerInstance;
    protected IAmCurrentUser CurrentUser;
    


    public BaseController(ILogger<T> loggerInstance, IAmCurrentUser currentUser)
    {
        LoggerInstance = loggerInstance;
        CurrentUser = currentUser;
        Token = CurrentUser.GetToken().Result;
        
    }
}