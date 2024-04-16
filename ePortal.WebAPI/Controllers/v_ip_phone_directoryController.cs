using ePortal.WebAPI.Context;
using ePortal.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class v_ip_phone_directoryController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public v_ip_phone_directoryController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<v_ip_phone_directoryDTO>>> GetPhoneDirectory()
        {
            var directory = await _context.v_ip_phone_directory.ToListAsync();

            var mappedPhoneDirectory = directory
                .GroupBy(d => new { d.Line })
                .Select(area => new v_ip_phone_directoryDTO
                {
                    //line
                    Line = area.Key.Line,
                    v_ip_phone_directoryArea = area
                    .GroupBy(o => new { o.Area })
                    .Select(office => new v_ip_phone_directoryAreaDTO
                    {
                        //area
                        Area = office.Key.Area,
                        v_ip_phone_directoryOffice = office
                        .GroupBy(a => new { a.Office, a.Office_Abbreviation })
                        .Select(office_area => new v_ip_phone_directoryOfficeDTO
                        {
                            //office
                            Office = office_area.Key.Office,
                            Office_Abbreviation = office_area.Key.Office_Abbreviation,
                            v_ip_phone_directoryOfficeArea = office_area
                            .GroupBy(i => new { i.Office_Area, i.Line_Number })
                            .Select(ip_phone => new v_ip_phone_directoryOfficeAreaDTO
                            { 
                                //ip-phone
                                Office_Area = ip_phone.Key.Office_Area,
                                Line_Number = ip_phone.Key.Line_Number
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();

            return mappedPhoneDirectory;
        }
    }
}
