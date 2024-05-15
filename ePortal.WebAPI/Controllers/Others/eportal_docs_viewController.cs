using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.Others
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
        public async Task<ActionResult<IEnumerable<eportal_docs_view>>> GetOfficeViewAnnouncementsForTheMonth()
        {
            // Get the current month and year
            var currentDate = DateTime.Today;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            // Define the list of document types to include
            var documentTypes = new List<string> { "ADMINISTRATIVE ORDER", "NOTICE", "EXECUTIVE ORDER", "MEMORANDUM" };

            // Filter announcements for the current month and year
            var announcements = await _context.eportal_docs_view
                .Where(a => a.docdate.HasValue && a.docdate.Value.Month == currentMonth && a.docdate.Value.Year == currentYear)
                .ToListAsync();

            // Get distinct documents with the highest docid for each title
            var groupedAnnouncements = announcements
                .GroupBy(a => a.title)
                .Select(g => g.OrderByDescending(a => a.docid).First())
                .OrderByDescending(a => a.docdate)
                .ToList();

            // Filter the grouped announcements by the specified document types
            var filteredAnnouncements = groupedAnnouncements
                .Where(a => documentTypes.Contains(a.doctype))
                .ToList();

            // Map the filtered announcements
            var mappedAnnouncements = filteredAnnouncements
                .Select(announcement => new eportal_docs_view
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
