using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class UnitsController : ApiBaseController<UnitsController>
{
    public UnitsController(ILogger<UnitsController> logger, IMediator mediator) : base(logger, mediator)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertTower( UpsertUnitDTO model)
    {
        return Ok(await _mediator.Send(new UpsertUnitCommand(model.UnitId, model.UnitTypeId, model.ScenicViewId,model.UnitNo, model.FloorId, model.FloorArea, model.Price)));
    }
}