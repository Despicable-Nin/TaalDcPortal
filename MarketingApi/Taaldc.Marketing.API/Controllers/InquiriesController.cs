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

   [AllowAnonymous]
   [HttpGet("{id}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesErrorResponseType(typeof(BadRequestResult))]
   public async Task<IActionResult> GetInquiryById(int id)
   {
      var inquiry = await _dbContext.Inquiries.Include(i => i.Customer).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

      if (inquiry == default) return NoContent();

      InquiryDto result = new()
      {
         Id = inquiry.Id,
         DateOfInquiry = inquiry.CreatedOn,
         DateVerified = inquiry.CreatedOn == inquiry.ModifiedOn ? null : inquiry.ModifiedOn,
         Country = inquiry.Customer.Country,
         Message = inquiry.Message,
         Property = inquiry.Property,
         Province = inquiry.Customer?.Province,
         Remarks = inquiry.Remarks,
         Salutation = inquiry.Customer?.Salutation,
         Status = inquiry.Status?.Name,
         ContactNo = inquiry.Customer?.ContactNo,
         EmailAddress = inquiry.Customer?.EmailAddress,
         FirstName = inquiry.Customer?.FirstName,
         LastName = inquiry.Customer?.LastName,
         InquiryType = inquiry.TypeOfInquiry,
         PropertyId = inquiry.PropertyId,
         TownCity = inquiry.Customer?.TownCity,
         AttendBy = inquiry.AttendedBy,
         InquiryTypeId = 0 //used to be a table but made into a string
      };

      return Ok(result);
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
       await _dbContext.SaveEntitiesAsync();
      return Ok();
   }
   

}