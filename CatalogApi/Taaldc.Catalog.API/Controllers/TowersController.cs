using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.DTO;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Controllers;

public class TowersController : ApiBaseController<TowersController>
{
    public TowersController(ILogger<TowersController> logger, IMediator mediator) : base(logger, mediator)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower( UpsertTowerDTO model)
    {
        return Ok(await _mediator.Send(new UpsertTowerCommand(model.PropertyId, model.Name, model.Address, model.TowerId)));
    }
}