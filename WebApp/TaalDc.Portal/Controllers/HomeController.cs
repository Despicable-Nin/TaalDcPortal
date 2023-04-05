using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Models;
using TaalDc.Portal.Services;

namespace WebApplication2.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISalesService _salesService;

    public HomeController(ILogger<HomeController> logger, ISalesService salesService)
    {
        _logger = logger;
        _salesService = salesService;
    }

    public async Task<IActionResult> Index()
    {
        //Get Units Stats
        var unitCountSummary = await _salesService.GetResidentialUnitsCountByStatus();
        var unitPerType = await _salesService.GetResidentialAvailabilityByType();
        var unitPerView = await _salesService.GetResidentialAvailabilityByView();

        var unitStatus = new UnitStats_ClientDto(unitCountSummary, unitPerType, unitPerView);

        return View(unitStatus);
    }

    public async Task<IActionResult> Parking()
    {
        //Get Units Stats
        var unitCountSummary = await _salesService.GetParkingUnitsCountByStatus();
        var unitPerFloor = await _salesService.GetParkingUnitTypeAvailabilityPerFloor();
        var unitPerType = await _salesService.GetAvailabilityPerParkingUnitType();

        var parkingStatus = new ParkingStats_ClientDto(unitCountSummary, unitPerFloor, unitPerType);

        return View(parkingStatus);
    }

    public async Task<IActionResult> FloorPlans()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult PageNotFound()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}