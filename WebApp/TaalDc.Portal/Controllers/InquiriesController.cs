using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.DTO.Marketing;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Controllers;

[Authorize]
public class InquiriesController : BaseController<InquiriesController>
{
    private readonly IMarketingService _marketingService;

    public InquiriesController(IMarketingService marketingService, ILogger<InquiriesController> loggerInstance) :
        base(loggerInstance)
    {
        _marketingService = marketingService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _marketingService.GetInquiries(10, 1);
        return View(result);
    }


    public async Task<IActionResult> Details(int id)
    {
        var inquiry = await _marketingService.GetInquiryById(id);

        return View(inquiry);
    }
}