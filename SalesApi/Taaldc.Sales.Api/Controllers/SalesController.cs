using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using Taaldc.Sales.API.Application.Commands.AddPayment;
using Taaldc.Sales.API.Application.Commands.ProcessPayment;
using Taaldc.Sales.API.Application.Commands.SellUnit;
using Taaldc.Sales.Api.DTO;

namespace Taaldc.Sales.Api.Controllers
{
    [Route("api/sel/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;
        private readonly IAmCurrentUser _currentUser;


        public SalesController(IMediator mediator, ILogger<SalesController> logger, IMapper mapper, IAmCurrentUser currentUser)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> Post([FromBody] SellUnitDTO dto)
        {
            SellUnitCommand command = _mapper.Map<SellUnitCommand>(dto);
            var result = await _mediator.Send(command);
            

            
            return Ok(result);
        }
        
        [HttpGet("payment/{id}/approve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> AcceptPayment(int id)
        {
            //this is for verification purposes --- only admin can do this
            //for now manually check role of user.. 
            AcceptPaymentCommand command = new AcceptPaymentCommand(id);
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPost("{id}/payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> AddPayment(int id, [FromBody] AddPaymentDTO dto)
        {
            if (id != dto.TransactionId) return BadRequest("Invalid request path.");
            
            AddPaymentCommand command = _mapper.Map<AddPaymentCommand>(dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
