using Microsoft.AspNetCore.Mvc;

namespace TaalDc.Portal.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult AcknowledgmentReceipt()
        {
            ViewData["Layout"] = null;
            return View();
        }
    }
}
