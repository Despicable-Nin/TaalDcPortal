using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertUnitType;
using Taaldc.Catalog.API.Application.Queries.References;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class UnitTypesController : ApiBaseController<UnitTypesController>
{
    private readonly IUnitTypeQueries _unitTypeQueries;

    public UnitTypesController(ILogger<UnitTypesController> logger, IMediator mediator,
        IUnitTypeQueries unitTypeQueries) : base(logger, mediator)
    {
        _unitTypeQueries = unitTypeQueries;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertUnitType(UnitTypeUpsert_HostDto model)
    {
        return Ok(
            await _mediator.Send(
                new UpsertUnitTypeCommand(model.UnitTypeId, model.Name, model.ShortCode)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetUnitTypes()
    {
        try
        {
            var unitTypes = await _unitTypeQueries.GetUnitTypes();
            return Ok(unitTypes);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
}