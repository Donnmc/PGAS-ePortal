using ePortal.WebAPI.Context;
using ePortal.WebAPI.DTO.ePortal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.ePortal.Views
{
    [Route("api/[controller]")]
    [ApiController]
    public class v_emergency_hotlineController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public v_emergency_hotlineController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<v_emergency_hotlineOfficeDTO>>> GetEmergencyHotline()
        {
            var directory = await _context.v_emergency_hotline.ToListAsync();

            var mappedHotelineDirectory = directory
                .GroupBy(o => new { o.Office, o.Office_Abbreviation })
                .Select(office => new v_emergency_hotlineOfficeDTO
                {
                    //office
                    Office = office.Key.Office,
                    Office_Abbreviation = office.Key.Office_Abbreviation,
                    v_emergency_hotlineLine = office
                    .GroupBy(l => new { l.Line })
                    .Select(line => new v_emergency_hotlineLineDTO
                    {
                        //line
                        Line = line.Key.Line,
                        v_emergency_hotlineArea = line
                        .Select(hotline => new v_emergency_hotlineDTO
                        {
                            //hotline
                            Area = hotline.Area,
                            Hotline = hotline.Hotline
                        }).ToList()
                    }).ToList()
                }).ToList();

            return mappedHotelineDirectory;
        }
    }
}
