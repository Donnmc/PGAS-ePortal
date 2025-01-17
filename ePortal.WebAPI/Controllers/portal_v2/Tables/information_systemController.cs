using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.portal_v2.Table;
using PGAS.WebAPI.Context;

namespace PGAS.WebAPI.Controllers.portal_v2.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class information_systemController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public information_systemController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/information_system
        [HttpGet]
        public async Task<ActionResult<IEnumerable<information_system>>> Getinformation_system()
        {
            return await _context.information_system.ToListAsync();
        }

        // GET: api/information_system/5
        [HttpGet("{id}")]
        public async Task<ActionResult<information_system>> Getinformation_system(int id)
        {
            var information_system = await _context.information_system.FindAsync(id);

            if (information_system == null)
            {
                return NotFound();
            }

            return information_system;
        }

        // PUT: api/information_system/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putinformation_system(int id, information_system information_system)
        {
            if (id != information_system.id)
            {
                return BadRequest();
            }

            _context.Entry(information_system).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!information_systemExists(id))
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

        // POST: api/information_system
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<information_system>> Postinformation_system(information_system information_system)
        {
            _context.information_system.Add(information_system);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (information_systemExists(information_system.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getinformation_system", new { information_system.id }, information_system);
        }

        // DELETE: api/information_system/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteinformation_system(int id)
        {
            var information_system = await _context.information_system.FindAsync(id);
            if (information_system == null)
            {
                return NotFound();
            }

            _context.information_system.Remove(information_system);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool information_systemExists(int id)
        {
            return _context.information_system.Any(e => e.id == id);
        }
    }
}
