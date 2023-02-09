using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using Taaldc.Sales.API.Application.Commands.AddPayment;
using Taaldc.Sales.API.Application.Commands.ProcessPayment;
using Taaldc.Sales.API.Application.Commands.SellUnit;
using Taaldc.Sales.Api.Application.Commands.VoidPayment;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Api.DTO;

namespace Taaldc.Sales.Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v1/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IAmCurrentUser _currentUser;
    private readonly ILogger<SalesController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IOrderQueries _orderQueries;


    public SalesController(IMediator mediator, IOrderQueries orderQueries, ILogger<SalesController> logger,
        IMapper mapper, IAmCurrentUser currentUser)
    {
        _mediator = mediator;
        _orderQueries = orderQueries;
        _logger = logger;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> Post([FromBody] SellUnitDTO dto)
    {
        if (_currentUser.Roles.Any() && _currentUser.Roles.Contains("ADMIN")) dto.Broker = dto.Broker ?? "In-house";

        var command = _mapper.Map<SellUnitCommand>(dto);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("{id}/payments/{paymentId}/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> AcceptPayment(int id, int paymentId)
    {
        //this is for verification purposes --- only admin can do this
        //for now manually check role of user.. 

        if (_currentUser.Roles.Any() && _currentUser.Roles.Contains("ADMIN"))
        {
            var command = new AcceptPaymentCommand(id, paymentId);
            return Ok(await _mediator.Send(command));
        }

        return BadRequest("Unauthorized.");
    }


    [HttpPost("{id}/payments/{paymentId}/void")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> VoidPayment(int id, int paymentId)
    {
        //this is for verification purposes --- only admin can do this
        //for now manually check role of user.. 

        if (_currentUser.Roles.Any() && _currentUser.Roles.Contains("ADMIN"))
        {
            var command = new VoidPaymentCommand(id, paymentId);
            return Ok(await _mediator.Send(command));
        }

        return BadRequest("Unauthorized.");
    }

    [HttpPost("{id}/payments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> AddPayment(int id, [FromBody] AddPaymentDTO dto)
    {
        if (id != dto.TransactionId) return BadRequest("Invalid request path.");

        var command = _mapper.Map<AddPaymentCommand>(dto);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}/payments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPayments(int id)
    {
        return Ok(await _orderQueries.GetPayments(id));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPresellingUnits(
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int unitStatus = 1,
        int pageNumber = 1,
        int pageSize = 20,
        string broker = ""
    )
    {
        if (unitStatus <= 0) return BadRequest("Invalid unit status");
        if (unitStatus > 4) return BadRequest("Invalid unit status");

        return Ok(await _orderQueries.GetUnitAndOrdersByAvailability(unitStatus, pageNumber, pageSize, floorId,
            unitTypeId, viewId, broker));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPresellingUnits(
        int id
    )
    {
        if (id <= 0) return BadRequest("Invalid order id");

        return Ok(await _orderQueries.GetOrder(id));
    }
}