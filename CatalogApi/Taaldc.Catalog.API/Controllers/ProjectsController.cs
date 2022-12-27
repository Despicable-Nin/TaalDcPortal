using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertProject;
using Taaldc.Catalog.API.Application.Commands.UpsertProperty;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class ProjectsController : ApiBaseController<ProjectsController>
{
    public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertProject(UpsertProjectDTO model)
    {
        return Ok(await _mediator.Send(new UpsertProjectCommand(model.ProjectId, model.Name, model.Developer)));
    }

    [HttpPost("{id}/properties")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertProperty(int id, UpsertPropertyDTO model)
    {
        return Ok(await _mediator.Send(new UpsertPropertyCommand(model.PropertyId, id, model.Name, model.LandArea)));
    }
}