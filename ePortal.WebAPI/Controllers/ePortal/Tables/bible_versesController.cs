using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using ePortal.WebAPI.DTO.ePortal;

namespace ePortal.WebAPI.Controllers.ePortal.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class bible_versesController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public bible_versesController(pgas_eportal_v2Context context)
        {
            _context = context;
        }
        // GET: api/bible_verses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BibleVerseDTO>>> Getbible_verses()
        {
            return await _context.bible_verses
                .Select(verse => new BibleVerseDTO
                {
                    Verse = verse.verse,
                    Chapter = verse.chapter,
                    Date = verse.date,
                    Archive = verse.archive
                })
                .ToListAsync();
        }

        [HttpGet("latest")]
        public async Task<ActionResult<BibleVerseLatestDTO>> GetLatestBibleVerse()
        {
            var latestVerse = await _context.bible_verses
                .OrderByDescending(verse => verse.date)
                .Select(verse => new BibleVerseLatestDTO
                {
                    Verse = verse.verse,
                    Chapter = verse.chapter,
                    Date = verse.date,
                })
                .FirstOrDefaultAsync();

            if (latestVerse == null)
            {
                return NotFound(); // Return a 404 Not Found if no verses are found
            }

            return latestVerse;
        }

        // GET: api/bible_verses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<bible_verses>> Getbible_verses(int id)
        {
            var bible_verses = await _context.bible_verses.FindAsync(id);

            if (bible_verses == null)
            {
                return NotFound();
            }

            return bible_verses;
        }

        // PUT: api/bible_verses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbible_verses(int id, bible_verses bible_verses)
        {
            if (id != bible_verses.id)
            {
                return BadRequest();
            }

            _context.Entry(bible_verses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bible_versesExists(id))
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

        // POST: api/bible_verses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<bible_verses>> Postbible_verses(bible_verses bible_verses)
        {
            _context.bible_verses.Add(bible_verses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbible_verses", new { bible_verses.id }, bible_verses);
        }

        // DELETE: api/bible_verses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletebible_verses(int id)
        {
            var bible_verses = await _context.bible_verses.FindAsync(id);
            if (bible_verses == null)
            {
                return NotFound();
            }

            _context.bible_verses.Remove(bible_verses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool bible_versesExists(int id)
        {
            return _context.bible_verses.Any(e => e.id == id);
        }
    }
}
