using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.DTO;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Controllers;

public class TowersController : ApiBaseController<TowersController>
{
	private readonly IUnitQueries _unitQueries;

	public TowersController(ILogger<TowersController> logger, IMediator mediator, IUnitQueries unitQueries) : base(logger, mediator)
    {
        _unitQueries = unitQueries;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower( UpsertTowerDTO model)
    {
        return Ok(await _mediator.Send(new UpsertTowerCommand(model.PropertyId, model.Name, model.Address, model.TowerId)));
    }

	[HttpGet("{id}/available-units-by-type")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(BadRequestResult))]
	public async Task<IActionResult> GetAvailabilityByUnitType(int id)
	{
		return Ok(await _unitQueries.GetUnitTypeAvailabilityByTowerId(id));
	}
}