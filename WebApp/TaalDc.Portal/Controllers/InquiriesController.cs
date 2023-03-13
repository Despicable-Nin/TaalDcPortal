using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Marketing;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Controllers;


public class InquiriesController : BaseController<InquiriesController>
{
    private readonly IMarketingService _marketingService;

    public InquiriesController(IMarketingService marketingService, ILogger<InquiriesController> loggerInstance) :
        base(loggerInstance)
    {
        _marketingService = marketingService;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, int status = 1)
    {
        var result = await _marketingService.GetInquiries(pageSize, pageNumber, status);
        return View(result);
    }

    public async Task<IActionResult> Closed(int pageNumber = 1, int pageSize = 10, int status = 3)
    {
        var result = await _marketingService.GetInquiries(pageSize, pageNumber, status);
        return View(result);
    }


    public async Task<IActionResult> Details(int id)
    {
        var inquiry = await _marketingService.GetInquiryById(id);

        return View(inquiry);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateInquiryStatus(UpdateInquiryStatusDto dto)
    {
        await _marketingService.UpdateInquiryStatus(dto);

        return Ok();
    }
}