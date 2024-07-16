using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;

namespace PGAS.WebAPI.Controllers.portal_v2.images
{
    [Route("api/[controller]")]
    [ApiController]
    public class carousel_imageController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public carousel_imageController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        private readonly string _uploadsFolderPath = @"C:\Users\jonis\source\repos\PGAS Portal\ePortal.Client\wwwroot\Uploads";

        [HttpGet("images")]
        public IActionResult GetImages()
        {
            if (!Directory.Exists(_uploadsFolderPath))
            {
                return NotFound("Uploads folder not found.");
            }

            var images = Directory.GetFiles(_uploadsFolderPath)
                .Where(file => new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => new
                {
                    file_name = Path.GetFileName(file),
                })
                .ToList();

            return Ok(images);
        }
    }
}
