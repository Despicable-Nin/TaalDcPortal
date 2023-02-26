using Microsoft.AspNetCore.Mvc;

namespace TaalDc.Portal.Controllers
{
    public class DocumentsController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AcknowledgmentReceipt([FromServices] IWebHostEnvironment env)
        {
            // Get the path to the wwwroot folder
            var wwwrootPath = env.WebRootPath;

            string pdfTemplate = Path.Combine(wwwrootPath + "\\files\\forms\\ACKNOWLEDGMENT PDF.pdf");
            var dataDir = Path.Combine(wwwrootPath + "\\files\\buyers-form\\");

            return Ok();
        }
    }
}
