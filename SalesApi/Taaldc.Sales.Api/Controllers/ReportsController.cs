using Microsoft.AspNetCore.Mvc;
using Taaldc.Sales.Api.Application.Queries.Reports;

namespace Taaldc.Sales.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportQueries _reportQueries;

        public ReportsController(IReportQueries reportQueries)
        {
            _reportQueries = reportQueries;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrdersByDate(DateTime from, DateTime to)
        {
            return Ok(await _reportQueries.GetOrdersByDate(from, to));
        }

        [HttpGet("reserved-without-downpayment")]
        public async Task<IActionResult> GetReservationsWithoutDownPayments()
        {
            return Ok();
            
        }

        [HttpGet("expired-reservation-count")]
        public async Task<IActionResult> GetExpiredReservationsCount()
        {
            return Ok();
        }
    }
}
