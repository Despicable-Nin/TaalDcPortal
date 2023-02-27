using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Sales.Contracts;

namespace TaalDc.Portal.Controllers
{
    public class ContractsController : Controller
    {
        public IActionResult Create(int buyerId)
        {
            return View(buyerId);
        }

        [HttpPost]
        public IActionResult Create(ContractCreate_ClientDto model)
        {
            return Ok();
        }

        [HttpGet("{buyerId}")]
        public async Task<IActionResult> GetContractByBuyerId(int buyerId)
        {
            //TODO: GetContractResponse
            return Ok();
        }
    }
}
