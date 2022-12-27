using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class FloorsController : ApiBaseController<FloorsController>
{
    [HttpPost("{id}/units")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower(int id, UpsertUnitDTO model)
    {
        return Ok(await _mediator.Send(new UpsertUnitCommand(model.UnitId, model.UnitTypeId,model.ScenicViewId, model.UnitNo, id, model.FloorArea, model.Price)));
    }
}