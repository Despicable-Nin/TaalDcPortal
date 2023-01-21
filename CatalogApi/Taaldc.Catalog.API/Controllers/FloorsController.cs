using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries.Floors;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class FloorsController : ApiBaseController<FloorsController>
{
	private readonly IFloorQueries _floorQueries;

	public FloorsController(ILogger<FloorsController> logger, IMediator mediator, IFloorQueries floorQueries) : base(logger, mediator)
    {
        _floorQueries = floorQueries;
    }

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertFloor( UpsertFloorDTO model)
    {
        return Ok(await _mediator.Send(new UpsertFloorCommand(model.TowerId, model.FloorId,model.Name, model.Description, model.FloorPlanFilePath)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetActiveFloors(
        string filter,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        return Ok(await _floorQueries.GetActiveFloors(filter, sortBy, sortOrder, pageNumber, pageSize));
    }

    [AllowAnonymous]
    [HttpGet("available")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(BadRequestResult))]
	public async Task<IActionResult> GetAvailabilityByUnitType(int? unitTypeId)
	{
		return Ok(await _floorQueries.GetAvailableFloorsByUnitType(unitTypeId));
	}
}