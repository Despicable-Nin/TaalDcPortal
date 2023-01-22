using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Printing;
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
	public async Task<IActionResult> Index(int? floorId,
        int? unitTypeId,
        int? viewId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //

        var sales = await _salesService.GetUnitAndOrdersAvailability(2, pageNumber, pageSize, floorId, unitTypeId, viewId);
        return View(sales);
    }


    public async Task<IActionResult> Available(int? floorId,
        int? unitTypeId,
        int? viewId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //

        var sales = await _salesService.GetUnitAndOrdersAvailability(1, pageNumber, pageSize, floorId, unitTypeId, viewId);
        return View(sales);
    }


    public async Task<IActionResult> Reserved(
		int? floorId,
		int? unitTypeId,
		int? viewId,
		int pageNumber = 1,
		int pageSize = 10
		)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        
        var sales = await _salesService.GetUnitAndOrdersAvailability(3, pageNumber, pageSize, floorId, unitTypeId, viewId);


        return View(sales);
    }

    public async Task<IActionResult> Blocked(int? floorId,
        int? unitTypeId,
        int? viewId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //

        var sales = await _salesService.GetUnitAndOrdersAvailability(4, pageNumber, pageSize, floorId, unitTypeId, viewId);


        return View(sales);
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
        var salesCreateDTO = new SalesCreateDTO();
        return View(salesCreateDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SalesCreateDTO model)
	{
		if (ModelState.IsValid) { 
            var result = await _salesService.SellUnit(model);

		    if (!result.IsSuccess) return BadRequest(new { Message = result.ErrorMessage });

		    //Update Unit Status in Catalog
		    var unitStatus = new UnitStatusUpdateDTO(model.UnitId, 3, $"Reserved to {model.FirstName} {model.LastName}");
            var unitStatusResult = await _catalogService.UpdateUnitStatus(unitStatus);

            if(!unitStatusResult.IsSuccess) return BadRequest(new { Message = unitStatusResult.ErrorMessage });

		    return Ok(result);
        }
        else
        {
            return BadRequest(model);
        }
	}
    

    public async Task<IActionResult> Details(int id)
    {
        var result = await _salesService.GetSalesById(id);

        return View(result);
    }
    
    //we need to be able to call sales/sel/sales POST (SellUnit)
    //what to do with the result?
    //we can throw it in Hangfire here..
    // a job that queries for the unit in catalog and updates the status . . .
    //for now.. mae a call to HTTPclient here... to catalog api to udpate unit..
}