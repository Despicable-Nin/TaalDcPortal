using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Commands.ChangeStatusOfUnit;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.Application.Common.Models;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.References;
using Taaldc.Catalog.API.Application.Queries.Units;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.API.Controllers;

public class UnitsController : ApiBaseController<UnitsController>
{
    private readonly IUnitQueries _unitQueries;
    private readonly IMapper _mapper;


    public UnitsController(ILogger<UnitsController> logger, IMediator mediator, IUnitQueries unitQueries, IMapper mapper) : base(logger, mediator)
    {
        _unitQueries = unitQueries;
        _mapper = mapper;
    }

    protected UnitsController(IUnitQueries unitQueries, IMapper mapper)
    {
        _unitQueries = unitQueries;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetUnityId(int id)
    {
        return Ok(await _unitQueries.GetUnitById(id));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> UpsertUnit( UnitUpsert_HostDto dto)
    {
        var command = new UpsertUnitCommand(dto.UnitId, dto.UnitStatusId, dto.UnitTypeId, dto.ScenicViewId, dto.UnitNo,
            dto.FloorId, dto.FloorArea, dto.BalconyArea, dto.Price, dto.Remarks, dto.IsActive);

        return Ok(await _mediator.Send(command));
    }
    
    [HttpPost("change-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> ChangeStatusOfUnit( ChangeStatusOfUnitDTO model)
    {
        return Ok(await _mediator.Send(new ChangeStatusOfUnitCommand(model.UnitId, model.UnitStatus, model.Remarks)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetActiveUnits(
        string filter,
        int? floorId,
        int? unitTypeId,
        int? viewId,
        int? statusId,
        string sortBy,
        SortOrderEnum sortOrder,
        int pageNumber = 1,
        int pageSize = 10)
    {
        return Ok(await _unitQueries.GetActiveUnits(filter, floorId, unitTypeId, viewId, statusId, sortBy, sortOrder, pageNumber, pageSize));
    }

	[AllowAnonymous]
	[HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    public async Task<IActionResult> GetAvailableUnits(
        int? unitTypeId, 
        int? viewId, 
        int? floorId, 
        string location,
        int min = 0,
        int max = 999999999, 
        int pageSize = 20, 
        int pageNumber = 1)
    {
        return Ok(await _unitQueries.GetAvailableUnitsAsync(unitTypeId, viewId, floorId, location, min, max, pageSize, pageNumber));
    }
}