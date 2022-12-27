using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.DTO;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Controllers;

public class TowersController : ApiBaseController<TowersController>
{
    [HttpPost("{id}/floors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower(int id, UpsertFloorDTO model)
    {
        return Ok(await _mediator.Send(new UpsertFloorCommand(id, model.FloorId,model.Name, model.Description)));
    }
}