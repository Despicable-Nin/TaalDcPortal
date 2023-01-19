using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.ChangeStatusOfUnit;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.References;
using Taaldc.Catalog.API.Application.Queries.Units;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class UnitsController : ApiBaseController<UnitsController>
{
    private readonly IUnitQueries _unitQueries;

    public UnitsController(
        IUnitQueries unitQueries,
        ILogger<UnitsController> logger, 
        IMediator mediator) : base(logger, mediator)
    {
        _unitQueries = unitQueries;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertUnit( UpsertUnitDTO model)
    {
        return Ok(await _mediator.Send(new UpsertUnitCommand(model.UnitId, model.UnitTypeId, model.ScenicViewId,model.UnitNo, model.FloorId, model.FloorArea, model.BalconyArea, model.Price, model.Remarks)));
    }
    
    [HttpPost("change-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> ChangeStatusOfUnit( ChangeStatusOfUnitDTO model)
    {
        return Ok(await _mediator.Send(new ChangeStatusOfUnitCommand(model.UnitId, model.UnitStatus, model.Remarks)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetActiveUnits(
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
        return Ok(await _unitQueries.GetActiveUnits(filter, floorId, unitTypeId, viewId, statusId, sortBy, sortOrder, pageNumber, pageSize));
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableUnits(
        int? unitTypeId, 
        int? viewId, 
        int? floorId, 
        string location,
        int min = 0,
        int max = 999999999, 
        int pageSize = 20, 
        int pageNumber = 1)
    {
        return Ok(await _unitQueries.GetAvailableUnitsAsync(unitTypeId, viewId, floorId, location, min, max, pageSize, pageNumber));
    }
}