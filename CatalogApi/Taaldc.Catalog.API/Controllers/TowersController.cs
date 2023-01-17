using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.Towers;
using Taaldc.Catalog.API.DTO;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Controllers;

public class TowersController : ApiBaseController<TowersController>
{
	private readonly IUnitQueries _unitQueries;
    private readonly ITowerQueries _towerQueries;

    public TowersController(ILogger<TowersController> logger, 
        IMediator mediator, 
        IUnitQueries unitQueries,
        ITowerQueries towerQueries) : base(logger, mediator)
    {
        _unitQueries = unitQueries;
        _towerQueries = towerQueries;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower( UpsertTowerDTO model)
    {
        return Ok(await _mediator.Send(new UpsertTowerCommand(model.PropertyId, model.Name, model.Address, model.TowerId)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetActiveTowers(
        string filter, 
        string sortBy, 
        SortOrderEnum sortOrder, 
        int pageNumber = 1, 
        int pageSize = 10)
    {
        return Ok(await _towerQueries.GetActiveTowers(filter, sortBy, sortOrder, pageNumber, pageSize));
    }

    [HttpGet("{id}/available-units-by-type")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(BadRequestResult))]
	public async Task<IActionResult> GetAvailabilityByUnitType(int id)
	{
		return Ok(await _unitQueries.GetUnitTypeAvailabilityByTowerId(id));
	}

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetTowerById(int id)
    {
        var tower = await _towerQueries.GetTowerById(id);
        if (tower == null) return NotFound();
        return Ok(tower);
    }
}