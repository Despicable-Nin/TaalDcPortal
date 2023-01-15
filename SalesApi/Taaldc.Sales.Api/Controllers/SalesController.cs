using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.API.Application.Commands.SellUnit;
using Taaldc.Sales.Api.DTO;

namespace Taaldc.Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;


        public SalesController(IMediator mediator, ILogger<SalesController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> Post([FromBody] SellUnitDTO dto)
        {
            SellUnitCommand command = _mapper.Map<SellUnitCommand>(dto);
            var result = await _mediator.Send(command);
            
            //what to do with the result?
            //we can throw it in Hangfire here..
            // a job that queries for the unit in catalog and updates the status . . .
            
            return Ok();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentDTO dto)
        {
            return Ok();
        }
        
       
    }
}
