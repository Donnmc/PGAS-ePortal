using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.Entities.portal_v2.Table;

namespace PGAS.WebAPI.Controllers.portal_v2.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class praise_messageController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;
        private readonly TimeZoneInfo PhilippineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

        public praise_messageController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/praise_message
        [HttpGet("GetAllPraiseMessages")]
        public async Task<ActionResult<IEnumerable<praise_message>>> GetAllPraiseMessages()
        {
            var praiseMessages = await _context.praise_message
                                              .OrderByDescending(p => p.date) // Assuming "Date" is the property name
                                              .ToListAsync();
            return Ok(praiseMessages);
        }

        // GET: api/praise_message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<praise_message>> Getpraise_message(int id)
        {
            var praise_message = await _context.praise_message.FindAsync(id);

            if (praise_message == null)
            {
                return NotFound();
            }

            return praise_message;
        }

        // PUT: api/praise_message/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpraise_message(int id, praise_message praise_message)
        {
            if (id != praise_message.id)
            {
                return BadRequest();
            }

            _context.Entry(praise_message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!praise_messageExists(id))
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

        // POST: api/praise_message
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<praise_message>> PostPraiseMessage(
            [FromForm] long from_eid,
            [FromForm] long to_eid,
            [FromForm] string message,
            [FromForm] int stars)
        {
            var praiseMessage = new praise_message
            {
                from_eid = from_eid,
                to_eid = to_eid,
                message = message,
                stars = stars,
                date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PhilippineTimeZone),
                archive = false
            };

            _context.praise_message.Add(praiseMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PraiseMessageExists(praiseMessage.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostPraiseMessage), new { praiseMessage.id }, praiseMessage);
        }

        private bool PraiseMessageExists(int id)
        {
            return _context.praise_message.Any(e => e.id == id);
        }

        // DELETE: api/praise_message/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepraise_message(int id)
        {
            var praise_message = await _context.praise_message.FindAsync(id);
            if (praise_message == null)
            {
                return NotFound();
            }

            _context.praise_message.Remove(praise_message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool praise_messageExists(int id)
        {
            return _context.praise_message.Any(e => e.id == id);
        }
    }
}
