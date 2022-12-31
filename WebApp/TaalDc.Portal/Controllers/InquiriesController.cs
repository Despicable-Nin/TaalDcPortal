using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeedWork;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Controllers;

[Authorize]
public class InquiriesController : BaseController<InquiriesController>
{
    private readonly IMarketingService _marketingService;
    public InquiriesController(IMarketingService marketingService, ILogger<InquiriesController> loggerInstance, IAmCurrentUser currentUser) : base(loggerInstance, currentUser)
    {
        _marketingService = marketingService;
    }
    
    public async Task<IActionResult> Index()
    {
        var result = await _marketingService.GetInquiries(10, 1);
        return View(result);
    }
}