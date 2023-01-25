using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.UpsertProperty;
using Taaldc.Catalog.API.Application.Commands.UpsertTower;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries.Properties;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class PropertiesController : ApiBaseController<PropertiesController>
{
    private readonly IPropertyQueries _propertyQueries;

    public PropertiesController(ILogger<PropertiesController> logger, IMediator mediator, IPropertyQueries propertyQueries) : base(logger, mediator)
    {
        _propertyQueries = propertyQueries;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertProperty(PropertyUpsert_HostDto model)
    {
        return Ok(
            await _mediator.Send(
                new UpsertPropertyCommand(
                    model.PropertyId,
                    model.ProjectId,
                    model.Name,
                    model.LandArea)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableUnits(
        string filter, string sortBy, SortOrderEnum sortOrder, int pageNumber = 1, int pageSize = 10)
    {
        return Ok(await _propertyQueries.GetActiveProperties(filter, sortBy, sortOrder, pageNumber, pageSize));
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetPropertyById(int id)
    {
        var property = await _propertyQueries.GetPropertyById(id);
        if (property == null) return NotFound();
        return Ok(property);
    }
}