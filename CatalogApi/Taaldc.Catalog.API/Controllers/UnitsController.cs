using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.Units;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class UnitsController : ApiBaseController<UnitsController>
{
    private readonly IUnitQueries _unitQueries;
    public UnitsController(IUnitQueries unitQueries, ILogger<UnitsController> logger, IMediator mediator) : base(logger, mediator)
    {
        _unitQueries = unitQueries;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertUnit( UpsertUnitDTO model)
    {
        return Ok(await _mediator.Send(new UpsertUnitCommand(model.UnitId, model.UnitTypeId, model.ScenicViewId,model.UnitNo, model.FloorId, model.FloorArea, model.Price)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableUnits(int? unitTypeId, int? viewId, int? floorId, int min = 0,
        int max = 999999999, int pageSize = 20, int pageNumber = 1)
    {
        return Ok(await _unitQueries.GetAvailableUnitsAsync(unitTypeId, viewId, floorId, min, max, pageSize, pageNumber));
    }
}