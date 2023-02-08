using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly ILogger<StatusController> _logger;
    private readonly IOrderRepository _repository;

    public StatusController(IOrderRepository repository, ILogger<StatusController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("payment-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPaymentStatus()
    {
        return Ok(await _repository.GetPaymentStatus());
    }

    [HttpGet("payment-types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPaymentTypes()
    {
        return Ok(await _repository.GetPaymentTypes());
    }

    [HttpGet("sale-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetSaleStatus()
    {
        return Ok(await _repository.GetSaleStatus());
    }

    [HttpGet("payment-transaction-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPaymentTransactionType()
    {
        return Ok(await _repository.GetPaymentTransactionTypes());
    }
}