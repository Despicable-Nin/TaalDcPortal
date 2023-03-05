using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Controllers;

[Authorize("Custodian")]
public class PropertiesController : BaseController<PropertiesController>
{
    private readonly ICatalogService _catalogService;
    private readonly IMapper _mapper;

    public PropertiesController(ICatalogService catalogService, IMapper mapper,
        ILogger<PropertiesController> loggerInstance) : base(loggerInstance)
    {
        _catalogService = catalogService;
        _mapper = mapper;
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