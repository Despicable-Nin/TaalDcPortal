using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taaldc.Marketing.API.DTO;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;
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
         .Include(i => i.Status)
         .AsNoTracking()
         .ToListAsync();

      var records =  result.Select(i => new InquiryDto(i.Message, i.PropertyId, i.Property, i.AttendedBy, i.Remarks,
         0, i.TypeOfInquiry, i.Status.Id, i.Status.Name, i.Customer.Salutation, i.Customer.FirstName,
         i.Customer.LastName, i.Customer.EmailAddress, i.Customer.ContactNo, i.Customer.Country, i.Customer.Province,
         i.Customer.TownCity)).AsEnumerable();
      
      return Ok(records);
   }
   
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesErrorResponseType(typeof(BadRequestResult))]
   public async Task<IActionResult> PostInquiries( AddInquiryDto dto )
   {
      Inquiry entity = new(dto.InquiryType, dto.Message, dto.PropertyId, dto.Property, dto.Remarks,
         new Customer(dto.Salutation, dto.FirstName, dto.LastName, dto.EmailAddress, dto.ContactNo, dto.Country,
            dto.Province, dto.TownCity));
      
       _dbContext.Inquiries.Add(entity);
       await _dbContext.SaveChangesAsync();
      return Ok();
   }
   

}