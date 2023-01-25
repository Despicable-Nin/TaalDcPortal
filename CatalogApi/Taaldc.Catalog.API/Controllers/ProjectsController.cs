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
    public async Task<IActionResult> UpsertProject(ProjectUpsert_HostDto model)
    {
        return Ok(await _mediator.Send(new UpsertProjectCommand(model.ProjectId, model.Name, model.Developer)));
    }

 
}