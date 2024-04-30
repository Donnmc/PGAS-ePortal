using ePortal.WebAPI.Context;
using ePortal.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class m_vwGetAllEmployee_MinifiedController : ControllerBase
    {
        private readonly pmisContext _context;

        public m_vwGetAllEmployee_MinifiedController(pmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<m_vwGetAllEmployee_MinifiedDTO>>> GetEmployees()
        {
            var employee = await _context.m_vwGetAllEmployee_Minified.ToListAsync();

            var mappedEmployee = employee
                .Where(o => o.isactive == true)
                .GroupBy(o => new
                {
                    o.OfficeName,
                    o.OfficeAbbr,
                    o.EmpName,
                    o.SwipeId,
                    o.eid,
                    o.Position,
                    o.SG,
                    o.Status,
                    o.isactive
                })
                .OrderBy(o => o.Key.OfficeName).ThenByDescending(e => e.Key.SG).ThenByDescending(e => e.Key.Status)
                .Select(office => new m_vwGetAllEmployee_MinifiedDTO
                {
                    //Office
                    OfficeName = office.Key.OfficeName,
                    OfficeAbbr = office.Key.OfficeAbbr,
                    //Employee
                    EmpName = office.Key.EmpName,
                    SwipID = office.Key.SwipeId,
                    eid = office.Key.eid,
                    Position = office.Key.Position,
                    SG = office.Key.SG,
                    Status = office.Key.Status,
                    isactive = office.Key.isactive
                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<m_vwGetAllEmployee_MinifiedDTO>>> SearchEmployees(
        string searchOfficeName = null,
        string searchOfficeAbbr = null,
        string searchEmpName = null,
        string searchSwipID = null)
        {
            var query = _context.m_vwGetAllEmployee_Minified.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrEmpty(searchOfficeName))
            {
                query = query.Where(e => e.OfficeName.Contains(searchOfficeName));
            }

            if (!string.IsNullOrEmpty(searchOfficeAbbr))
            {
                query = query.Where(e => e.OfficeAbbr.Contains(searchOfficeAbbr));
            }

            if (!string.IsNullOrEmpty(searchEmpName))
            {
                query = query.Where(e => e.EmpName.Contains(searchEmpName));
            }

            if (!string.IsNullOrEmpty(searchSwipID))
            {
                query = query.Where(e => e.SwipeId.Contains(searchSwipID));
            }

            var employee = await query.Where(e => e.isactive == true).ToListAsync();

            var mappedEmployee = employee
                .Where(o => o.isactive == true)
                .GroupBy(o => new
                {
                    o.OfficeName,
                    o.OfficeAbbr,
                    o.EmpName,
                    o.SwipeId,
                    o.eid,
                    o.Position,
                    o.SG,
                    o.Status,
                    o.isactive
                })
                .OrderBy(o => o.Key.OfficeName).OrderByDescending(e => e.Key.SG).OrderByDescending(e => e.Key.Status)
                .Select(office => new m_vwGetAllEmployee_MinifiedDTO
                {
                    //Office
                    OfficeName = office.Key.OfficeName,
                    OfficeAbbr = office.Key.OfficeAbbr,
                    //Employee
                    EmpName = office.Key.EmpName,
                    SwipID = office.Key.SwipeId,
                    eid = office.Key.eid,
                    Position = office.Key.Position,
                    SG = office.Key.SG,
                    Status = office.Key.Status,
                    isactive = office.Key.isactive
                }).ToList();

            return mappedEmployee;
        }


    }
}
