using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Marketing.API.DTO;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;
using Taaldc.Marketing.Infrastructure;

namespace Taaldc.Marketing.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/mkt/[controller]")]
public class InquiriesController : Controller
{
   private readonly ILogger<InquiriesController> _logger;
   private readonly IAmCurrentUser _currentUser;
   private readonly MarketingDbContext _dbContext;

   public InquiriesController(ILogger<InquiriesController> logger, IAmCurrentUser currentUser, MarketingDbContext dbContext)
   {
      _logger = logger;
      _dbContext = dbContext;
      _currentUser = currentUser;
   }

   [HttpGet]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesErrorResponseType(typeof(BadRequestResult))]
   public async Task<IActionResult> GetInquiries(int pageSize = 10, int pageNumber = 1)
   {
      var result = await _dbContext.Inquiries
         .Include(i => i.Customer)
         .Include(i => i.Status)
         .AsNoTracking()
         .Skip(pageSize * (pageNumber -1))
         .Take(pageSize)
         .OrderBy(i => i.Id)
         .ToListAsync();

      var total = await _dbContext.Inquiries.AsNoTracking().CountAsync();

      var records =  result.Select(i => new InquiryDto
      {
         Id = i.Id,
         DateOfInquiry = i.CreatedOn,
         DateVerified = i.CreatedOn == i.ModifiedOn ? null : i.ModifiedOn,
         Country = i.Customer.Country,
         Message = i.Message,
         Property = i.Property,
         Province = i.Customer?.Province,
         Remarks = i.Remarks,
         Salutation = i.Customer?.Salutation,
         Status = i.Status?.Name,
         ContactNo = i.Customer?.ContactNo,
         EmailAddress = i.Customer?.EmailAddress,
         FirstName = i.Customer?.FirstName,
         LastName = i.Customer?.LastName,
         InquiryType = i.TypeOfInquiry,
         PropertyId = i.PropertyId,
         TownCity = i.Customer?.TownCity,
         AttendBy = i.AttendedBy,
         InquiryTypeId = 0 //used to be a table but made into a string
      }).AsEnumerable();
      
      return Ok(new InquriesResult{ PageSize =pageSize, PageNumber = pageNumber, Total = total, Inquiries = records.ToList()});
   }
   
   [AllowAnonymous]
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