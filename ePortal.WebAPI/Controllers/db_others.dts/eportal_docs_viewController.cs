using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.db_others.dts.View;
using PGAS.WebAPI.Context;

namespace PGAS.WebAPI.Controllers.Others
{
    [Route("api/[controller]")]
    [ApiController]
    public class eportal_docs_viewController : ControllerBase
    {
        private readonly othersContext _context;

        public eportal_docs_viewController(othersContext context)
        {
            _context = context;
        }

        [HttpGet("officeViewAnnouncements")]
        public async Task<ActionResult<IEnumerable<eportal_docs_viewDTO>>> GetOfficeViewAnnouncementsForTheMonth()
        {
            var announcements = await _context.eportal_docs_view.ToListAsync();

            var mappedAnnouncements = announcements
                .OrderByDescending(e => e.docdate)
                .Select(announcement => new eportal_docs_viewDTO
                {
                    title = announcement.title,
                    description = announcement.description,
                    doctype = announcement.doctype,
                    docdate = announcement.docdate,
                    OfficeName = announcement.OfficeName,
                    docid = announcement.docid,
                }).ToList();

            return mappedAnnouncements;
        }


    }
}
