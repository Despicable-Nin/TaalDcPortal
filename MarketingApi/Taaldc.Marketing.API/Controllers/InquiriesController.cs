using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using SendGrid.Helpers.Mail;
using SendGrid;
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
      var inquiry = await _dbContext.Inquiries
            .Include(i => i.Customer)
            .Include(i => i.Status)
            .AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

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
   public async Task<IActionResult> GetInquiries(int pageSize = 10, int pageNumber = 1, int status = 1)
   {
        var result = await _dbContext.Inquiries
            .Include(i => i.Customer)
            .Include(i => i.Status)
            .AsNoTracking()
            .Where(i => i.Status.Id == status)
            .Skip(pageSize * (pageNumber -1))
            .Take(pageSize)
            .OrderByDescending(i => i.CreatedOn)
            .ToListAsync();

      var total = await _dbContext.Inquiries.Where(i => i.Status.Id == status).AsNoTracking().CountAsync();

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
        try
        {
            Inquiry entity = new(dto.InquiryType, dto.Message, dto.PropertyId, dto.Property, dto.Remarks,
                new Customer(dto.Salutation, dto.FirstName, dto.LastName, dto.EmailAddress, dto.ContactNo, dto.Country,
                dto.Province, dto.TownCity));

            _dbContext.Inquiries.Add(entity);

            await _dbContext.SaveEntitiesAsync();
            await SendEmailToSales(dto, entity);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return Ok();
   }


    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(UpdateInquiryStatusDto model)
    {
        try
        {
            var inquiry = await _dbContext.Inquiries.FirstOrDefaultAsync(i => i.Id == model.Id);

            if (inquiry == null) return NotFound(new { Message = $"Inquiry with Id: {model.Id} not found" });

            inquiry.SetStatus(model.Status);
            inquiry.Attend(_currentUser.Name);

            await _dbContext.SaveEntitiesAsync();

            return Accepted();
            
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);

            return BadRequest(new
            {
                Success = false,
                Message = "Error occured while saving the data."
            });
        }
    }

    private static async Task SendEmailToSales(AddInquiryDto dto, Inquiry entity)
    {
        var salutationArray = dto.Salutation.Split(":");
        var withOtherSalutation = salutationArray[0] == "Other";

        var salutation = withOtherSalutation ? salutationArray[1] : salutationArray[0];

        var address = $"{dto.TownCity}, {dto.Province}, {dto.Country}";

        var body = $"<div dir=\"ltr\">\r\n<p><strong>Inquiry Date</strong><br />{entity.CreatedOn.ToString("MMMM dd, yyyy hh:mm tt")}</p>\r\n" +
            $"<p><strong>Full Name</strong><br />{salutation} {dto.FirstName} {dto.LastName}</p>\r\n" +
            $"<p><strong>Inquiry Type</strong><br />{dto.InquiryType}</p>\r\n" +
            $"<p><strong>Email Address</strong><br /><a href=\"{dto.EmailAddress}\" target=\"_blank\">{dto.EmailAddress}</a>" +
            $"</p>\r\n<p><strong>Contact No.</strong><br />{dto.ContactNo}</p>\r\n" +
            $"<p><strong>Address</strong><br />{address}</p>\r\n" +
            $"<p><strong>Property Inquired</strong><br />{dto.Property}</p>\r\n" +
            $"<p><strong>Message</strong><br />{dto.Message}</p>\r\n</div>";

        var apiKey = "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";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("taaldcwebsite@gmail.com", "Taaldc Website");
        var subject = $"New Inquiry Received - {dto.InquiryType}";

        var recipients = new List<EmailAddress>();
#if DEBUG

        recipients.Add(new EmailAddress("chrisdanevalla@gmail.com", "Chrisdan Evalla"));
#else
            recipients.Add(new EmailAddress("sales@taaldc.com.ph", "Sales"));
            recipients.Add(new EmailAddress("taalDC2010@gmail.com", "Taal DC"));
            recipients.Add(new EmailAddress("taaldcwebsite@gmail.com", "Taal DC"));
#endif
        var htmlContent = body;
        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, subject, "", htmlContent);
        var response = await client.SendEmailAsync(msg);
    }

}