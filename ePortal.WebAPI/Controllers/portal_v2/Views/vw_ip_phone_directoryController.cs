using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.DTO.portal_v2.View;

namespace PGAS.WebAPI.Controllers.ePortal.Views
{
    [Route("api/[controller]")]
    [ApiController]
    public class vw_ip_phone_directoryController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public vw_ip_phone_directoryController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<vw_ip_phone_directoryLineDTO>>> GetPhoneDirectory()
        {
            var directory = await _context.vw_ip_phone_directory.ToListAsync();

            var mappedPhoneDirectory = directory
                .GroupBy(d => new { d.Line })
                .Select(area => new vw_ip_phone_directoryLineDTO
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
                            .Select(ip_phone => new v_ip_phone_directoryDTO
                            {
                                //ip-phone
                                Office_Area = ip_phone.Office_Area,
                                Line_Number = ip_phone.Line_Number
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();

            return mappedPhoneDirectory;
        }
    }
}
