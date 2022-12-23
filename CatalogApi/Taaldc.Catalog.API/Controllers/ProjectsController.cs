using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IMediator _mediator;

    public ProjectsController(ILogger<ProjectsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertProject(UpsertProjectDTO model)
    {
        return Ok();
    }
}