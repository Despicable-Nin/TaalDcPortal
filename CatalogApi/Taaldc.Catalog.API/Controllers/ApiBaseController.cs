using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Taaldc.Catalog.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/adm/[controller]")]
public class ApiBaseController<T> : ControllerBase where T : ControllerBase
{
    protected readonly ILogger<T> _logger;
    protected readonly IMediator _mediator;


    public ApiBaseController(ILogger<T> logger, IMediator mediator) : this()
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected ApiBaseController()
    {
    }
}