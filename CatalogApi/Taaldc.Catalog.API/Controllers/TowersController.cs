using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.Towers;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class TowersController : ApiBaseController<TowersController>
{
    private readonly ITowerQueries _towerQueries;
    private readonly IUnitQueries _unitQueries;

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
    public async Task<IActionResult> UpsertTower(TowerUpsert_HostDto model)
    {
        return Ok(await _mediator.Send(new UpsertTowerCommand(model.PropertyId, model.Name, model.Address,
            model.TowerId)));
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

    [AllowAnonymous]
    [HttpGet("{id}/available-units-by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailabilityByUnitType(int id)
    {
        return Ok(await _unitQueries.GetUnitTypeAvailabilityByTowerIdAsync(id));
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