using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ePortal.WebAPI.Context;
using ePortal.WebAPI.DTO;
using ePortal.WebAPI.Entities;

namespace ePortal.WebAPI.Controllers.ePortal.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class information_system_clusterController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public information_system_clusterController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/information_system_cluster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<information_system_cluster>>> Getinformation_system_cluster()
        {
            return await _context.information_system_cluster.ToListAsync();
        }

        // GET: api/information_system_cluster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<information_system_cluster>> Getinformation_system_cluster(int id)
        {
            var information_system_cluster = await _context.information_system_cluster.FindAsync(id);

            if (information_system_cluster == null)
            {
                return NotFound();
            }

            return information_system_cluster;
        }

        // PUT: api/information_system_cluster/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putinformation_system_cluster(int id, information_system_cluster information_system_cluster)
        {
            if (id != information_system_cluster.id)
            {
                return BadRequest();
            }

            _context.Entry(information_system_cluster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!information_system_clusterExists(id))
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

        // POST: api/information_system_cluster
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<information_system_cluster>> Postinformation_system_cluster(information_system_cluster information_system_cluster)
        {
            _context.information_system_cluster.Add(information_system_cluster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getinformation_system_cluster", new { information_system_cluster.id }, information_system_cluster);
        }

        // DELETE: api/information_system_cluster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteinformation_system_cluster(int id)
        {
            var information_system_cluster = await _context.information_system_cluster.FindAsync(id);
            if (information_system_cluster == null)
            {
                return NotFound();
            }

            _context.information_system_cluster.Remove(information_system_cluster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool information_system_clusterExists(int id)
        {
            return _context.information_system_cluster.Any(e => e.id == id);
        }
    }
}
