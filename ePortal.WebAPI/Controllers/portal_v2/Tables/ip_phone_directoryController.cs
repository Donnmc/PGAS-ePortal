using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.portal_v2.Table;
using PGAS.WebAPI.Context;

namespace PGAS.WebAPI.Controllers.ePortal.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class ip_phone_directoryController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public ip_phone_directoryController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/ip_phone_directory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ip_phone_directory>>> Getip_phone_directory()
        {
            return await _context.ip_phone_directory.ToListAsync();
        }

        // GET: api/ip_phone_directory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ip_phone_directory>> Getip_phone_directory(int id)
        {
            var ip_phone_directory = await _context.ip_phone_directory.FindAsync(id);

            if (ip_phone_directory == null)
            {
                return NotFound();
            }

            return ip_phone_directory;
        }

        // PUT: api/ip_phone_directory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putip_phone_directory(int id, ip_phone_directory ip_phone_directory)
        {
            if (id != ip_phone_directory.id)
            {
                return BadRequest();
            }

            _context.Entry(ip_phone_directory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ip_phone_directoryExists(id))
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

        // POST: api/ip_phone_directory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ip_phone_directory>> Postip_phone_directory(ip_phone_directory ip_phone_directory)
        {
            _context.ip_phone_directory.Add(ip_phone_directory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getip_phone_directory", new { ip_phone_directory.id }, ip_phone_directory);
        }

        // DELETE: api/ip_phone_directory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteip_phone_directory(int id)
        {
            var ip_phone_directory = await _context.ip_phone_directory.FindAsync(id);
            if (ip_phone_directory == null)
            {
                return NotFound();
            }

            _context.ip_phone_directory.Remove(ip_phone_directory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ip_phone_directoryExists(int id)
        {
            return _context.ip_phone_directory.Any(e => e.id == id);
        }
    }
}
