using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.Services;
using WebApplication2.Controllers;

namespace TaalDc.Portal.Controllers
{
    [Authorize]
    public class BuyersController : Controller
    {
        private readonly ILogger<BuyersController> _logger;
        public BuyersController(ILogger<BuyersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QuickCreate()
        {
            return View();
        }
    }
}
