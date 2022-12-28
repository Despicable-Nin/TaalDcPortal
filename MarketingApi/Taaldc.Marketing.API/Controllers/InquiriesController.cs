using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taaldc.Marketing.Infrastructure;

namespace Taaldc.Marketing.API.Controllers;

[ApiController]
[Route("api/mkt/[controller]")]
public class InquiriesController : Controller
{
   private readonly ILogger<InquiriesController> _logger;
   private readonly MarketingDbContext _dbContext;

   public InquiriesController(ILogger<InquiriesController> logger, MarketingDbContext dbContext)
   {
      _logger = logger;
      _dbContext = dbContext;
   }

   [HttpGet]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesErrorResponseType(typeof(BadRequestResult))]
   public async Task<IActionResult> GetInquiries( )
   {
      var result = await _dbContext.Inquiries
         .Include(i => i.Customer)
         .Include(i => i.InquiryType)
         .Include(i => i.Status)
         .AsNoTracking()
         .ToListAsync();
      
      return Ok(result);
   }
}