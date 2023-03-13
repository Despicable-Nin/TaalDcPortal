using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Taaldc.Sales.API.Application.Commands.UpsertSpouse;
using Taaldc.Sales.Api.Application.Queries.Buyers;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Controllers
{
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BuyersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BuyersController> _logger;
        private readonly IAmCurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IBuyerQueries _buyerQueries;

        public BuyersController(IMediator mediator, ILogger<BuyersController> logger, IAmCurrentUser currentUser, IMapper mapper, IBuyerQueries buyerQueries)
        {
            _mediator = mediator;
            _logger = logger;
            _currentUser = currentUser;
            _mapper = mapper;
            _buyerQueries = buyerQueries;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetPaginatedBuyersAsync(
            string name,
            string email, 
            string createdBy,
            int? civilStatusId, 
            int pageSize = 10, 
            int pageNumber =1)
        {
            if (string.IsNullOrEmpty(createdBy)) { 
             createdBy = _currentUser.IsBroker()? _currentUser.Email: string.Empty;
            }

            return Ok(await _buyerQueries.GetPaginatedAsync(pageNumber, pageSize, name, email, civilStatusId, createdBy));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetBuyersAsync(int id)
        {
            return Ok(await _buyerQueries.GetBuyerByIdAsync(id));
        }
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> AddBuyerAsync([FromBody] AddBuyerCommand model, CancellationToken ct = default)
        {
            return Ok(await _mediator.Send(model, ct));
        }
        
        [HttpPut("{id}/basic-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerBasicInfoAsync(int id,[FromBody] UpdateBuyerBasicInfoCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/contact-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerContactDetailsAsync(int id,[FromBody] UpdateBuyerContactDetailsCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/miscellaneous")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerMiscellaneousAsync(int id,[FromBody] UpdateBuyerMiscCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
                
        [HttpPatch("{id}/address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerMiscellaneousAsync(int id,[FromBody] UpsertBuyerAddressCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        
        [HttpPut("{id}/spouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpserBuyerSpouse(int id,[FromBody] UpsertSpouseCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        [HttpPut("{id}/company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateBuyerCompanyAsync(int id,[FromBody] UpsertCompanyCommand model, CancellationToken ct = default)
        {
            if (id != model.BuyerId) throw new Exception("Invalid request.");
            
            return Ok(await _mediator.Send(model, ct) );
        }
        
        
    }
}
