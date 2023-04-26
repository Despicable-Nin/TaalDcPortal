using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.Api.Application.Queries.Dashboard;

namespace Taaldc.Sales.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardQueries _dashboardQueries;
    private readonly ILogger<DashboardController> _logger;


    public DashboardController(IDashboardQueries dashboardQueries, ILogger<DashboardController> logger)
    {
        _dashboardQueries = dashboardQueries;
        _logger = logger;
    }

    [HttpGet("residential/status-summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetUnitCountSummaryByStatus()
    {
        int[] unitTypes = { 2, 3, 4, 5, 8 };
        var result = await _dashboardQueries.GetCountByUnitStatus(unitTypes);
        return Ok(result);
    }


    [HttpGet("residential/available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableUnitCount()
    {
        return Ok(await _dashboardQueries.GetAvailableUnitCount());
    }

    [HttpGet("residential/reserved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetReservedUnitCount()
    {
        return Ok(await _dashboardQueries.GetReservedUnitCount());
    }

    [HttpGet("residential/sold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetSoldUnitCount()
    {
        return Ok(await _dashboardQueries.GetSoldUnitCount());
    }

    [HttpGet("residential/blocked")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetBlockedUnitCount()
    {
        return Ok(await _dashboardQueries.GetBlockedUnitCount());
    }


    [HttpGet("parking/status-summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetParkingCountSummaryByStatus()
    {
        int[] unitTypes = { 6, 7 };
        var result = await _dashboardQueries.GetCountByUnitStatus(unitTypes);
        return Ok(result);
    }


    [HttpGet("parking/available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableParkingCount()
    {
        return Ok(await _dashboardQueries.GetAvailableParkingCount());
    }

    [HttpGet("parking/reserved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetReservedParkingCount()
    {
        return Ok(await _dashboardQueries.GetReservedParkingCount());
    }

    [HttpGet("parking/sold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetSoldParkingCount()
    {
        return Ok(await _dashboardQueries.GetSoldParkingCount());
    }

    [HttpGet("parking/blocked")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetBlockedParkingCount()
    {
        return Ok(await _dashboardQueries.GetBlockedParkingCount());
    }

    [HttpGet("parking/available-per-floor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetParkingUnitTypeAvailabilityPerFloor()
    {
        return Ok(await _dashboardQueries.GetParkingUnitTypeAvailabilityPerFloor());
    }

    [HttpGet("parking/available-per-unit-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailabilityPerParkingUnitType()
    {
        return Ok(await _dashboardQueries.GetAvailabilityPerParkingUnitType());
    }


    [HttpGet("residential/available-per-view")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailabilityOfResidentialUnitPerView()
    {
        return Ok(await _dashboardQueries.GetResidentaialUnitAvailabilityPerView());
    }

    [HttpGet("residential/available-per-unit-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailabilityPerResidentialUnitType()
    {
        return Ok(await _dashboardQueries.GetAvailabilityPerResidentialUnitType());
    }
    
    
}