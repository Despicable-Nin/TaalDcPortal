using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaalDc.Portal.Controllers
{
    [Authorize]
    public class UploadController : BaseController<UploadController>
    {
        public UploadController(ILogger<UploadController> loggerInstance) : base(loggerInstance)
        {
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> SaveFile(string folder, [FromServices] IWebHostEnvironment env)
        {
            var file = Request.Form.Files.GetFile("file");
            
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            //if (file == null || file.Length == 0)
            //{
            //    return BadRequest("Invalid file.");
            //}

            // Get the path to the wwwroot folder
            var wwwrootPath = env.WebRootPath;

            // Combine the wwwroot path with the file name to create a unique file path

            var newFileName = GenerateFileName(file.FileName);
            var filePath = Path.Combine(wwwrootPath + "\\files" + $"\\{folder}", newFileName);
            var friendlyPath = Path.Combine("\\files" + $"\\{folder}", newFileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(friendlyPath);
        }

        private static string GenerateFileName(string fileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{Path.GetExtension(fileName)}";

            return newFileName;
        }
    }
}
