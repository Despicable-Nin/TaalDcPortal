using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Controllers;

[Authorize]
public class SalesController : BaseController<SalesController>
{
	private readonly ISalesService _salesService;
	private readonly ICatalogService _catalogService;

	public SalesController(ISalesService salesService, ICatalogService catalogService, ILogger<SalesController> loggerInstance) : base(loggerInstance)
	{
		_salesService = salesService;
		_catalogService = catalogService;
	}

	// GET
	public IActionResult Index()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //
        
        
        return View();
    }


    public IActionResult Available()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //


        return View();
    }


    public IActionResult Reserved()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //


        return View();
    }

    public IActionResult Blocked()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //


        return View();
    }

    public IActionResult Cancelled()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //


        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SalesCreateDTO model)
    {
        if (model.DownPayment == 0) model.DownpaymentConfirmNo = "-";

        if(string.IsNullOrEmpty(model.Remarks)) model.Remarks = "";
        
        var result = await _salesService.SellUnit(model);

		if (!result.IsSuccess) return BadRequest(new { Message = result.ErrorMessage });

		//Update Unit Status in Catalog
		var unitStatus = new UnitStatusUpdateDTO(model.UnitId, 3, $"Reserved to {model.FirstName} {model.LastName}");
        var unitStatusResult = await _catalogService.UpdateUnitStatus(unitStatus);

        if(!unitStatusResult.IsSuccess) return BadRequest(new { Message = unitStatusResult.ErrorMessage });

		return Ok(result);
	}
    
    
    //we need to be able to call sales/sel/sales POST (SellUnit)
    //what to do with the result?
    //we can throw it in Hangfire here..
    // a job that queries for the unit in catalog and updates the status . . .
    //for now.. mae a call to HTTPclient here... to catalog api to udpate unit..
}