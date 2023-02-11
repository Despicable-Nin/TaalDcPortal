using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Sales;
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

        [HttpPost]
        public IActionResult Create(BuyerCreate_ClientDto model)
        {
            //Check if model is valid
            //If valid, send request to API
            //If not, return back the with error
            return RedirectToAction("Index");
        }
    }
}
