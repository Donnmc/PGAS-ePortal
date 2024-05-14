using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.Others
{
    [Route("api/[controller]")]
    [ApiController]
    public class t_doc_office_vwController : ControllerBase
    {
        private readonly othersContext _context;

        public t_doc_office_vwController(othersContext context)
        {
            _context = context;
        }

        [HttpGet("memorandums")]
        public async Task<ActionResult<IEnumerable<t_doc_office_vw>>> getMemorandumAnnouncements()
        {
            var announcements = await _context.t_doc_public_vw.ToListAsync();

            var mappedAnnouncements = announcements
                .OrderByDescending(a => a.docdate)
                .Select(document => new t_doc_office_vw
                {
                    title = document.title,
                    description = document.description,
                    docdate = document.docdate,
                    doctype = document.doctype,
                    OfficeName = document.OfficeName,
                }).ToList();

            return mappedAnnouncements;
        }
    }
}
