using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SeedWork;
using Taaldc.Sales.API.Application.Commands.AddBuyer;
using Taaldc.Sales.API.Application.Commands.UpdateBuyerBasicInfo;
using Taaldc.Sales.API.Application.Commands.UpdateBuyerContactDetails;
using Taaldc.Sales.API.Application.Commands.UpdateBuyerMisc;
using Taaldc.Sales.API.Application.Commands.UpsertBuyerAddress;
using Taaldc.Sales.API.Application.Commands.UpsertCompany;
using Taaldc.Sales.Api.Application.Queries.Buyers;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
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
        
        [HttpPut("{id}/basic-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerBasicInfo(int id,[FromBody] UpdateBuyerBasicInfoCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/contact-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerContactDetails(int id,[FromBody] UpdateBuyerContactDetailsCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/miscellaneous")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerMiscellaneous(int id,[FromBody] UpdateBuyerMiscCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
                
        [HttpPatch("{id}/address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerMiscellaneous(int id,[FromBody] UpsertBuyerAddressCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerCompany(int id,[FromBody] UpsertCompanyCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
    }
}
