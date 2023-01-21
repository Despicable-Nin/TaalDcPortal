using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taaldc.Catalog.API.Application.Queries.Floors;
using Taaldc.Catalog.API.Application.Queries.ScenicViews;

namespace Taaldc.Catalog.API.Controllers
{
	public class ScenicViewsController : ApiBaseController<ScenicViewsController>
	{
		private readonly IScenicViewQueries _scenicViewQueries;

		public ScenicViewsController(ILogger<ScenicViewsController> logger, IMediator mediator, IScenicViewQueries scenicViewQueries) : base(logger, mediator)
		{
			_scenicViewQueries = scenicViewQueries;
		}

		[AllowAnonymous]
		[HttpGet("available")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(BadRequestResult))]
		public async Task<IActionResult> GetAvailabilityByUnitType(int? unitTypeId)
		{
			return Ok(await _scenicViewQueries.GetAvailableViewsByUnitType(unitTypeId));
		}
	}
}
