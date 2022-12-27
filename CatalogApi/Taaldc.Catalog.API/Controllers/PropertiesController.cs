using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertProperty;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class PropertiesController : ApiBaseController<PropertiesController>
{
    public PropertiesController(ILogger<PropertiesController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertProperty(UpsertPropertyDTO model)
    {
        return Ok(
            await _mediator.Send(
                new UpsertPropertyCommand(
                    model.PropertyId, 
                    model.ProjectId, 
                    model.Name,
                    model.LandArea)));
    }
}