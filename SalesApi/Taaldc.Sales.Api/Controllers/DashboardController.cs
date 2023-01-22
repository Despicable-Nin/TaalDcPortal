using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.Api.Application.Queries.Dashboard;

namespace Taaldc.Sales.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardQueries _dashboardQueries;
        private readonly ILogger<DashboardController> _logger;


        public DashboardController(IDashboardQueries dashboardQueries, ILogger<DashboardController> logger)
        {
            _dashboardQueries = dashboardQueries;
            _logger = logger;
        }

        [HttpGet("residential/available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetAvailableUnitCount() => Ok(await _dashboardQueries.GetAvailableUnitCount());
        
        [HttpGet("residential/reserved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetReservedUnitCount() => Ok(await _dashboardQueries.GetReservedUnitCount());
        
        [HttpGet("residential/sold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetSoldUnitCount() => Ok(await _dashboardQueries.GetSoldUnitCount());
        
        [HttpGet("residential/blocked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetBlockedUnitCount() => Ok(await _dashboardQueries.GetBlockedUnitCount());
        
        [HttpGet("parking/available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetAvailableParkingCount() => Ok(await _dashboardQueries.GetAvailableParkingCount());
        
        [HttpGet("parking/reserved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetReservedParkingCount() => Ok(await _dashboardQueries.GetReservedParkingCount());
        
        [HttpGet("parking/sold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetSoldParkingCount() => Ok(await _dashboardQueries.GetSoldParkingCount());
        
        [HttpGet("parking/blocked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetBlockedParkingCount() => Ok(await _dashboardQueries.GetBlockedParkingCount());

        [HttpGet("parking/available-per-floor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetParkingUnitTypeAvailabilityPerFloor() =>
            Ok(await _dashboardQueries.GetParkingUnitTypeAvailabilityPerFloor());
        
        [HttpGet("parking/available-per-unit-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetAvailabilityPerParkingUnitType() =>
            Ok(await _dashboardQueries.GetAvailabilityPerParkingUnitType());
        
        
        [HttpGet("residential/available-per-view")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetAvailabilityOfResidentialUnitPerView() =>
            Ok(await _dashboardQueries.GetResidentaialUnitAvailabilityPerView());
        
        [HttpGet("residential/available-per-unit-type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> GetAvailabilityPerResidentialUnitType() =>
            Ok(await _dashboardQueries.GetAvailabilityPerResidentialUnitType());

    }
}
