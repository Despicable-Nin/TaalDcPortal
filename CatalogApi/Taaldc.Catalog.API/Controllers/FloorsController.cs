using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertFloor;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class FloorsController : ApiBaseController<FloorsController>
{
    public FloorsController(ILogger<FloorsController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower( UpsertFloorDTO model)
    {
        return Ok(await _mediator.Send(new UpsertFloorCommand(model.TowerId, model.FloorId,model.Name, model.Description)));
    }
}