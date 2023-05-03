﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Drawing.Printing;
using System.Globalization;
using System.Reflection;
using System.Text.Encodings.Web;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Controllers;

[Authorize]
public class PropertiesController : BaseController<PropertiesController>
{
    private readonly ICatalogService _catalogService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public PropertiesController(ICatalogService catalogService, IMapper mapper,
        ILogger<PropertiesController> loggerInstance, IConfiguration configuration) : base(loggerInstance)
    {
        _catalogService = catalogService;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var properties = await _catalogService.GetProperties(filter, sortBy, sortOrder, pageNumber, pageSize);

        return View(properties);
    }

    public async Task<IActionResult> CreateProperty()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty(PropertyCreate_ClientDto model)
    {
        var result = await _catalogService.CreateProperty(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });


        return Ok();
    }

    public async Task<IActionResult> EditProperty(int id)
    {
        try
        {
            var result = await _catalogService.GetPropertyById(id);

            if (result == null) RedirectToAction("Index");

            var propertyCreateClientDto = _mapper.Map<PropertyCreate_ClientDto>(result);

            return View(propertyCreateClientDto);
        }
        catch (Exception err)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditProperty(PropertyCreate_ClientDto model)
    {
        var result = await _catalogService.CreateProperty(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });


        //TODO Update Unit Replica

        return Ok(result);
    }

    [Authorize("LimitedCustodian")]
    public async Task<IActionResult> Towers(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var towers = await _catalogService.GetTowers(filter, sortBy, sortOrder, pageNumber, pageSize);

        return View(towers);
    }

    public async Task<IActionResult> CreateTower()
    {
        var properties = await _catalogService.GetProperties(null, null, 0, 1, 10000);

        ViewData["Properties"] = properties.Data;

        return View();
    }

    public async Task<IActionResult> CreateTower(TowerCreate_ClientDto model)
    {
        var result = await _catalogService.CreateTower(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });

        return Ok(result);
    }

    public async Task<IActionResult> EditTower(int id)
    {
        try
        {
            var result = await _catalogService.GetTowerById(id);

            if (result == null) RedirectToAction("Index");

            var properties = await _catalogService.GetProperties(null, null, 0, 1, 10000);

            ViewData["Properties"] = properties.Data;

            var towerCreateDTO = _mapper.Map<TowerCreate_ClientDto>(result);
            return View(towerCreateDTO);
        }
        catch (Exception err)
        {
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> CreateFloor()
    {
        var towers = await _catalogService.GetTowers(null, null, 0, 1, 10000);

        ViewData["Towers"] = towers.Data;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFloor(FloorCreate_ClientDto model)
    {
        var result = await _catalogService.CreateFloor(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });

        return Ok(result);
    }

    [AllowAnonymous]
    public async Task<IActionResult> FloorPlan(int id)
    {
        var result = await _catalogService.GetFloorById(id);
        var floorCreateDTO = _mapper.Map<FloorCreate_ClientDto>(
              result);
        return View(floorCreateDTO);
    }

    public async Task<IActionResult> EditFloor(int id)
    {
        try
        {
            var result = await _catalogService.GetFloorById(id);

            if (result == null) RedirectToAction("Floors");

            var towers = await _catalogService.GetTowers(null, null, 0, 1, 10000);

            ViewData["Towers"] = towers.Data;

            var floorCreateDTO = _mapper.Map<FloorCreate_ClientDto>(
                result);

            return View(floorCreateDTO);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Floors");
        }
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetFloorUnitsStatus(int id)
    {
        var result = await _catalogService.GetFloorUnitsStatus(id);
        return Ok(result);
    }

    public async Task<IActionResult> Floors(string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var floors = await _catalogService.GetFloors(filter, sortBy, sortOrder, pageNumber, pageSize);

        return View(floors);
    }

    public async Task<IActionResult> CreateUnit()
    {
        var floors = await _catalogService.GetFloors(null, null, 0, 1, 1000);

        ViewData["Floors"] = floors.Data;

        var unitTypes = await _catalogService.GetUnitTypes();

        ViewData["UnitTypes"] = unitTypes;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUnit(UnitCreate_ClientDto model)
    {
        var result = await _catalogService.CreateUnit(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });

        return Ok(result);
    }

    public async Task<IActionResult> EditUnit(int id)
    {
        var result = await _catalogService.GetUnitById(id);

        if (result == null) RedirectToAction("Units");

        var unitUpdateDto = _mapper.Map<UnitUpdate_ClientDto>(result);

        var floors = await _catalogService.GetFloors(null, null, 0, 1, 1000);

        ViewData["Floors"] = floors.Data;

        var unitTypes = await _catalogService.GetUnitTypes();

        ViewData["UnitTypes"] = unitTypes;

        return View(unitUpdateDto);
    }

    [HttpPost]
    public async Task<IActionResult> EditUnit(int id, UnitUpdate_ClientDto model)
    {
        LoggerInstance.LogDebug(JsonConvert.SerializeObject(model));

        if (id != model.UnitId) return BadRequest("Invalid id.");

        LoggerInstance.LogDebug($"Invoke {nameof(CatalogService.UpdateUnit)}");

        var result = await _catalogService.UpdateUnit(model);

        LoggerInstance.LogCritical($"Result is: {result.IsSuccess}. {result.ErrorMessage}");
        
        var floors = await _catalogService.GetFloors(null, null, 0, 1, 1000);

        ViewData["Floors"] = floors.Data;


        if (!result.IsSuccess) {
            TempData["Message"] = result.ErrorMessage.ToString();
            return View(model);
        } 
       
        return RedirectToAction("Units");

    }

    [Authorize("LimitedCustodian")]
    public async Task<IActionResult> Units(
        string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var units = await _catalogService.GetUnits(filter, floorId, unitTypeId, viewId, statusId, sortBy, sortOrder,
            pageNumber, pageSize);

        var unitTypes = await _catalogService.GetUnitTypes();

        ViewData["UnitTypes"] = unitTypes;

        return View(units);
    }

    [Authorize("LimitedCustodian")]
    public async Task<IActionResult> GetUnits(
        string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var units = await _catalogService.GetUnits(filter, floorId, unitTypeId, viewId, statusId, sortBy, sortOrder,
            pageNumber, pageSize);
        return new JsonResult(units);
    }


    public async Task<IActionResult> UnitsReport(string filter,
        int? floorId,
    int? unitTypeId,
    int? viewId,
    int? statusId)
    {
        var units = await _catalogService.GetUnits(filter, floorId, unitTypeId, viewId, statusId, "", SortOrderEnum.ASC,
            1, 99999);

        byte[] excelBytes = CreateUnitExcelFile(units.Data.ToList());
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Units Report.xlsx");
    }


    public byte[] CreateUnitExcelFile(List<Unit_ClientDto> orders)
    {
        // Create a new Excel package
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (ExcelPackage excelPackage = new ExcelPackage())
        {
            // Add a new worksheet to the Excel package
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Units");

            // Write the header row
            int headerRow = 1;
            PropertyInfo[] properties = typeof(Unit_ClientDto).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[headerRow, i + 1].Value = properties[i].Name;
            }

            // Write the data rows
            int dataRow = 2;
            foreach (var obj in orders)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    object value = properties[i].GetValue(obj);

                    if (properties[i].PropertyType == typeof(DateTime) || properties[i].PropertyType == typeof(DateTimeOffset))
                    {
                        worksheet.Cells[dataRow, i + 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    }

                    worksheet.Cells[dataRow, i + 1].Value = value;
                }
                dataRow++;
            }


            // Convert the Excel package to a byte array
            byte[] excelBytes = excelPackage.GetAsByteArray();
            return excelBytes;
        }
    }


    [Authorize("LimitedCustodian")]
    public async Task<IActionResult> UnitTypes()
    {
        var unitTypes = await _catalogService.GetUnitTypes();

        return View(unitTypes);
    }

    public IActionResult CreateUnitType()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUnitType(UnitTypeCreate_ClientDto model)
    {
        var result = await _catalogService.CreateUnitType(model);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                Message = result.ErrorMessage
            });

        return Ok(result);
    }
}