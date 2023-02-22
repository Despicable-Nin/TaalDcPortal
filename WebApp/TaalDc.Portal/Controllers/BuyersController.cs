using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Enums;
using TaalDc.Portal.DTO.Sales.Buyer;
using TaalDc.Portal.Services;
using WebApplication2.Controllers;

namespace TaalDc.Portal.Controllers
{
    [Authorize]
    public class BuyersController : Controller
    {
        private readonly ILogger<BuyersController> _logger;
        private readonly ISalesService _salesService;

        public BuyersController(ILogger<BuyersController> logger
            ,ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
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
        public async Task<IActionResult> Create(BuyerCreate_ClientDto model)
        {
            //Check if model is valid
            //If valid, send request to API
            //If not, return back the with error
            if(ModelState.IsValid)
            {
                var buyer = new BuyerCreateAPI_ClientDto(
                    model.Salutation
                   , model.FirstName
                   , model.MiddleName
                   , model.LastName
                   , model.EmailAddress
                   , ""
                   , model.MobileNo
                   , DateTime.MinValue
                   , 1
                   , new AddressDto(model.HomeAddress, model.HomeCity, model.HomeState, model.HomeCountry, model.HomeZipCode)
                   , model.IsCorporate
                   ,new CompanyDto(model.CompanyName, model.CompanyAddress, model.CompanyIndustry, "", "", "", "", "", "", "", "")
                   );

                try
                {
                    var result = await _salesService.AddBuyer(buyer);
                }catch(Exception err)
                {
                    return BadRequest(new
                    {
                        IsFormError = false,
                        err.Message
                    });
                }
            }

            return Ok();
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

            var spouse = new Buyer_ClientDto("Mrs.", "Anna", "Craig", "Doe", new DateTime(1990, 1, 1), CivilStatusEnum.Married);

            buyer.SpouseId = 2;
            spouse.Id = 2;
            buyer.Spouse = spouse;

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

        [HttpPost]
        public IActionResult EditSpouse(BuyerSpouseEdit_ClientDto model)
        {
            return Ok();
        }
    }
}
