﻿using System.Diagnostics;
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
    private readonly ICatalogService _catalogService;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger
        ,ISalesService salesService
        , ICatalogService catalogService
        ,IConfiguration configuration)
    {
        _logger = logger;
        _salesService = salesService;
        _catalogService = catalogService;
        _configuration = configuration; 
    }

    public async Task<IActionResult> Index()
    {
        //Get Units Stats
        var unitCountSummary = await _salesService.GetResidentialUnitsCountByStatus();
        var unitPerType = await _salesService.GetResidentialAvailabilityByType();
        var unitPerView = await _salesService.GetResidentialAvailabilityByView();

        var activeFloors = await _catalogService.GetActiveFloorsByTowerId(0);

        ViewData["ActiveFloors"] = activeFloors.OrderBy(a => a.Id);

        var unitStatus = new UnitStats_ClientDto(unitCountSummary, unitPerType, unitPerView);

        var expiredReservationCount = await _salesService.GetExpiredReservationCount();

        ViewData["ExpiredReservationCount"] = expiredReservationCount;

        return View(unitStatus);
    }

    public async Task<IActionResult> Parking()
    {
        //Get Units Stats
        var unitCountSummary = await _salesService.GetParkingUnitsCountByStatus();
        var unitPerFloor = await _salesService.GetParkingUnitTypeAvailabilityPerFloor();
        var unitPerType = await _salesService.GetAvailabilityPerParkingUnitType();

        var parkingStatus = new ParkingStats_ClientDto(unitCountSummary, unitPerFloor, unitPerType);


        var activeFloors = await _catalogService.GetActiveFloorsByTowerId(0);

        ViewData["ActiveFloors"] = activeFloors.OrderBy(a => a.Id);


        return View(parkingStatus);
    }

    public async Task<IActionResult> FloorPlans()
    {
        var activeFloors = await _catalogService.GetActiveFloorsByTowerId(0);

        ViewData["ActiveFloors"] = activeFloors;

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