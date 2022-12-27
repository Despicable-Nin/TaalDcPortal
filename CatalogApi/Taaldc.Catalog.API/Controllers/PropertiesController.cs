using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class PropertiesController : ApiBaseController<PropertiesController>
{
    public PropertiesController(ILogger<PropertiesController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpPost("{id}/towers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower(int id, UpsertTowerDTO model)
    {
        return Ok(await _mediator.Send(new UpsertTowerCommand(id, model.Name, model.Address, model.TowerId)));
    }
}