using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Enums;
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


        public IActionResult Details(int id)
        {
            var buyer = new Buyer_ClientDto("Mr.", "John", "Smith", "Doe", new DateTime(1990, 1, 1), CivilStatusEnum.Married);

            buyer.Id = id;

            buyer.SetContactDetails("johndoe@testmail.com", "09121234567", "");

            buyer.SetIDInformation("Consultant", "315 408 1234", "National ID", new DateTime(2027, 12, 31));

            buyer.HomeAddress = new ClientAddress("Home Address", "Manila", "NCR", "Philippines", "1234");
            buyer.BillingAddress = new ClientAddress("Billing Address", "Manila", "NCR", "Philippines", "1234");
            buyer.BusinessAddress = new ClientAddress("", "", "", "", "");

            buyer.IsCorporate = true;
            buyer.Company = new Company("John Doe, Inc.", "Manila City, NCR", "Accounting");

            return View(buyer);
        }


        public IActionResult Contracts(int id) {
            return View();
        }


        [HttpPost]
        public IActionResult EditGeneralInfo(BuyerGeneralInfoEdit_ClientDto model)
        {
            return Ok();
        }


        [HttpPost]
        public IActionResult EditContactInfo(BuyerContactInfoEdit_ClientDto model)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult EditAddress(BuyerAddressEdit_ClientDto model)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult EditIDInformation(BuyerIDInformationEdit_ClietnDto model)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult EditCompanyInformation(BuyerCompanyEdit_ClientDto model)
        {
            return Ok();
        }
    }
}
