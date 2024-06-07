using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.Others
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

        // GET: api/CarouselImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<carousel_image>>> GetCarouselImages()
        {
            return await _context.carousel_image.ToListAsync();
        }

        private readonly string _uploadsFolderPath = @"C:\Users\jonis\source\repos\PGAS ePortal\ePortal.Client\wwwroot\Uploads";

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

        // GET: api/CarouselImages/5
        [HttpGet("{file_name}")]
        public async Task<ActionResult<carousel_image>> GetCarouselImage(int file_name)
        {
            var carouselImage = await _context.carousel_image.FindAsync(file_name);

            if (carouselImage == null)
            {
                return NotFound();
            }

            return carouselImage;
        }


        // PUT: api/CarouselImages/5
        [HttpPut("{file_name}")]
        public async Task<IActionResult> PutCarouselImage(string file_name, carousel_image carouselImage)
        {
            if (file_name != carouselImage.file_name)
            {
                return BadRequest();
            }

            _context.Entry(carouselImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarouselImageExists(file_name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/CarouselImages/5
        [HttpDelete("{file_name}")]
        public async Task<IActionResult> DeleteCarouselImage(string file_name)
        {
            var carouselImage = await _context.carousel_image.FindAsync(file_name);
            if (carouselImage == null)
            {
                return NotFound();
            }

            _context.carousel_image.Remove(carouselImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarouselImageExists(string file_name)
        {
            return _context.carousel_image.Any(e => e.file_name == file_name);
        }
    }
}
