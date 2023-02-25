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

        public async Task<IActionResult> Index(int pageNumber=1, int pageSize=10, string name = "", string email = "")
        {
            var buyers = await _salesService.GetBuyers(pageNumber, pageSize, name, email);

            return View(buyers);
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


        public async Task<IActionResult> Details(int id)
        {
            var buyerQueryResult = await _salesService.GetBuyer(id);
            
            var buyer = new Buyer_ClientDto(
                buyerQueryResult.Salutation
                ,buyerQueryResult.FirstName
                ,buyerQueryResult.MiddleName
                ,buyerQueryResult.LastName
                ,buyerQueryResult.Dob
                ,buyerQueryResult.CivilStatusId
                ,buyerQueryResult.CivilStatus
                );

            buyer.Id = id;

            buyer.SetContactDetails(buyerQueryResult.EmailAddress, buyerQueryResult.MobileNo, buyerQueryResult.PhoneNo);

            buyer.SetIDInformation(buyerQueryResult.Occupation, buyerQueryResult.Tin, buyerQueryResult.GovIssuedId, buyerQueryResult.GovIssuedIdValidUntil);

            buyer.HomeAddress = new ClientAddress(
                    buyerQueryResult.HomeAddress_Street, 
                    buyerQueryResult.HomeAddress_City, 
                    buyerQueryResult.HomeAddress_State, 
                    buyerQueryResult.HomeAddress_Country, 
                    buyerQueryResult.HomeAddress_ZipCode);

            buyer.BillingAddress = new ClientAddress(
                    buyerQueryResult.BillingAddress_Street,
                    buyerQueryResult.BillingAddress_City,
                    buyerQueryResult.BillingAddress_State,
                    buyerQueryResult.BillingAddress_Country,
                    buyerQueryResult.BillingAddress_ZipCode);

            buyer.BusinessAddress = new ClientAddress(
                    buyerQueryResult.BusinessAddress_Street,
                    buyerQueryResult.BusinessAddress_City,
                    buyerQueryResult.BusinessAddress_State,
                    buyerQueryResult.BusinessAddress_Country,
                    buyerQueryResult.BusinessAddress_ZipCode);

            buyer.IsCorporate = buyerQueryResult.IsCorporate;

            buyer.Company = new Company(buyerQueryResult.Company_Name, 
                buyerQueryResult.Company_Address,
                buyerQueryResult.Company_Industry,
                buyerQueryResult.Company_PhoneNo,
                buyerQueryResult.Company_MobileNo,
                buyerQueryResult.Company_FaxNo,
                buyerQueryResult.Company_EmailAddress,
                buyerQueryResult.Company_TIN,
                buyerQueryResult.Company_SECRegNo,
                buyerQueryResult.Company_President,
                buyerQueryResult.Company_CorpSec);

            buyer.SpouseId = buyerQueryResult.PartnerId;

            if (buyerQueryResult.PartnerId.HasValue) {
                var spouse = new Buyer_ClientDto("Mrs.", "Anna", "Craig", "Doe", new DateTime(1990, 1, 1), 2, "Married");

                spouse.Id = buyerQueryResult.PartnerId.Value;

                buyer.Spouse = spouse;
            }

            return View(buyer);
        }


        public IActionResult Contracts(int id) {
            return View(id);
        }


        [HttpPost]
        public async Task<IActionResult> EditGeneralInfo(BuyerGeneralInfoEdit_ClientDto model)
        {
            await _salesService.UpdateBuyerInfo(model);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> EditContactInfo(BuyerContactInfoEdit_ClientDto model)
        {
            await _salesService.UpdateBuyerContact(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(BuyerAddressEdit_ClientDto model)
        {
            await _salesService.PatchBuyerAddress(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditIDInformation(BuyerIDInformationEdit_ClietnDto model)
        {
            await _salesService.UpdateBuyerMisc(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditCompanyInformation(BuyerCompanyEdit_ClientDto model)
        {
            model.Name = model.Name ?? string.Empty;
            model.Address = model.Address ?? string.Empty;
            model.Industry =  model.Industry ?? string.Empty;
            model.PhoneNo   = model.PhoneNo ?? string.Empty;
            model.MobileNo= model.MobileNo ?? string.Empty;
            model.FaxNo =  model.FaxNo ?? string.Empty;
            model.EmailAddress = model.EmailAddress ?? string.Empty;
            model.Tin = model.Tin ?? string.Empty;
            model.SecRegNo = model.SecRegNo ?? string.Empty;
            model.President = model.President ?? string.Empty;
            model.CorpSec = model.CorpSec ?? string.Empty;

            await _salesService.UpdateBuyerCompany(model);

            return Ok();
        }

        [HttpPost]
        public IActionResult EditSpouse(BuyerSpouseEdit_ClientDto model)
        {
            return Ok();
        }
    }
}
