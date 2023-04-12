using Microsoft.AspNetCore.Mvc;
using SeedWork;
using TaalDc.Portal.DTO.Sales.Contracts;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Controllers
{
    public class ContractsController : BaseController<ContractsController>
    {
		private readonly IAccountService _accountService;
		private readonly ICatalogService _catalogService;
		private readonly IAmCurrentUser _currentUser;
		private readonly ISalesService _salesService;


		public ContractsController(ILogger<ContractsController> loggerInstance, ISalesService salesService,
		ICatalogService catalogService, IAccountService accountService, IAmCurrentUser currentUser) : base(
		loggerInstance)
		{
			_salesService = salesService;
			_catalogService = catalogService;
			_accountService = accountService;
			_currentUser = currentUser;
		}

		public async Task<IActionResult> Create(int id)
        {
            var activeFloors = await _catalogService.GetActiveFloorsByTowerId(0);

            ViewData["ActiveFloors"] = activeFloors.OrderBy(a => a.Id);

            return View(id);
        }

        [HttpPost]
		public async Task<IActionResult> Create(CreateContractRequest model)
		{
			if (ModelState.IsValid)
			{
				var result = await _salesService.CreateContract(model);

				if (!result.IsSuccess)
					return BadRequest(new
					{
						IsFormError = false,
						Message = result.ErrorMessage,
						result.Id
					});

				return Ok(result);
			}

			return BadRequest(new
			{
				IsFormError = true,
				Message = "Please check your data entry.",
				ModelState = Json(ModelState)
			});
		}

        //[HttpGet("{buyerId}")]
        //public async Task<IActionResult> GetContractByBuyerId(int buyerId)
        //{
        //    //TODO: GetContractResponse
        //    return Ok();
        //}
    }
}
