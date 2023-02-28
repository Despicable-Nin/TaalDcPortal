using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Sales.Buyer;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly ISalesService _salesService;

        public DocumentsController(ILogger<DocumentsController> logger
            , ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }


        //[HttpPost]
        //public async Task<IActionResult> AcknowledgmentReceipt([FromServices] IWebHostEnvironment env)
        //{
        //    // Get the path to the wwwroot folder
        //    var wwwrootPath = env.WebRootPath;

        //    string pdfTemplate = Path.Combine(wwwrootPath + "\\files\\forms\\ACKNOWLEDGMENT PDF.pdf");
        //    var dataDir = Path.Combine(wwwrootPath + "\\files\\buyers-form\\");

        //    return Ok();
        //}

        public IActionResult AcknowledgmentReceipt(int id)
        {
            return View();
        }


        public async Task<IActionResult> BuyerInformationSheet(int buyerId, string type)
        {
            var buyerQueryResult = await _salesService.GetBuyer(buyerId);

            var buyer = new Buyer_ClientDto(
              buyerQueryResult.Salutation
              , buyerQueryResult.FirstName
              , buyerQueryResult.MiddleName
              , buyerQueryResult.LastName
              , buyerQueryResult.Dob
              , buyerQueryResult.CivilStatusId
              , buyerQueryResult.CivilStatus
              );

            buyer.Id = buyerId;

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

            if (buyerQueryResult.PartnerId.HasValue && buyerQueryResult.PartnerId.Value > 0)
            {
                var spouseResult = await _salesService.GetBuyer(buyerQueryResult.PartnerId.Value);
                var spouse = new Buyer_ClientDto(spouseResult.Salutation, spouseResult.FirstName, spouseResult.MiddleName, spouseResult.LastName, spouseResult.Dob, 2, "Married");

                spouse.SetContactDetails(spouseResult.EmailAddress, spouseResult.MobileNo, spouseResult.PhoneNo);

                spouse.SetIDInformation(spouseResult.Occupation, spouseResult.Tin, spouseResult.GovIssuedId, spouseResult.GovIssuedIdValidUntil);

                spouse.Id = buyerQueryResult.PartnerId.Value;

                buyer.Spouse = spouse;
            }

            return View(buyer);
        }
    }
}
