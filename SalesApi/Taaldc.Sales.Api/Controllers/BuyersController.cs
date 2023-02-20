using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using Taaldc.Sales.API.Application.Commands.AddBuyer;
using Taaldc.Sales.Api.Application.Queries.Buyers;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BuyersController> _logger;
        private readonly IAmCurrentUser _currentUser;

        public BuyersController(IMediator mediator, ILogger<BuyersController> logger, IAmCurrentUser currentUser)
        {
            _mediator = mediator;
            _logger = logger;
            _currentUser = currentUser;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> AddBuyer([FromBody] AddBuyerCommand model, CancellationToken ct = default)
        {
            return Ok(await _mediator.Send(model, ct));
        }
    }
}
