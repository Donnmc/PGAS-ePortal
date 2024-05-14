using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;

namespace ePortal.WebAPI.Controllers.ePortal.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class praise_messageController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public praise_messageController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/praise_message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<praise_message>>> Getpraise_message()
        {
            return await _context.praise_message.ToListAsync();
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
        public async Task<ActionResult<praise_message>> Postpraise_message(praise_message praise_message)
        {
            _context.praise_message.Add(praise_message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (praise_messageExists(praise_message.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getpraise_message", new { praise_message.id }, praise_message);
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
