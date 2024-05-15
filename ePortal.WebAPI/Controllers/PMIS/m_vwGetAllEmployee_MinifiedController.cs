using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.PMIS
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
        public async Task<ActionResult<IEnumerable<m_vwGetAllEmployee_Minified>>> GetEmployees()
        {
            var employee = await _context.m_vwGetAllEmployee_Minified.ToListAsync();

            var statusOrder = new[] {
                "Elected",
                "Permanent",
                "Casual",
                "Job Order",
                "Contract of Service",
                "Coterminous",
                "Temporary",
                "Detailed"
            };

            var mappedEmployee = employee
                .Where(o => o.isactive == true)
                .OrderBy(o => o.OfficeName) // Order by OfficeName
                .ThenBy(e =>  // Order statuses in the desired order (descending)
                    Array.IndexOf(statusOrder, e.Status) // Get index based on the order
                )
                .ThenByDescending(e => e.SG) // Order by SG (descending)
                .Select(office => new m_vwGetAllEmployee_Minified
                {
                    //Office
                    OfficeName = office.OfficeName,
                    OfficeAbbr = office.OfficeAbbr,
                    //Employee
                    EmpName = office.EmpName,
                    SwipeID = office.SwipeID,
                    eid = office.eid,
                    Position = office.Position,
                    SG = office.SG,
                    Status = office.Status,
                    isactive = office.isactive
                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<m_vwGetAllEmployee_Minified>>> SearchEmployees(
        string? searchDetails = null)
        {
            var query = _context.m_vwGetAllEmployee_Minified.AsQueryable();

            if (!string.IsNullOrEmpty(searchDetails))
            {
                query = query.Where(e =>
                    e.OfficeName.Contains(searchDetails) ||
                    e.OfficeAbbr.Contains(searchDetails) ||
                    e.Position.Contains(searchDetails) ||
                    e.EmpName.Contains(searchDetails) ||
                    e.SwipeID.Contains(searchDetails));
            }

            var employee = await query.Where(e => e.isactive == true).ToListAsync();

            var statusOrder = new[] {
                "Elected",
                "Permanent",
                "Casual",
                "Job Order",
                "Contract of Service",
                "Coterminous",
                "Temporary",
                "Detailed"
            };
            var mappedEmployee = employee
                .Where(o => o.isactive == true)
                .OrderBy(o => o.OfficeName) // Order by OfficeName
                .ThenBy(e =>  // Order statuses in the desired order (descending)
                    Array.IndexOf(statusOrder, e.Status) // Get index based on the order
                )
                .ThenByDescending(e => e.SG) // Order by SG (descending)
                .Select(office => new m_vwGetAllEmployee_Minified
                {
                    //Office
                    OfficeName = office.OfficeName,
                    OfficeAbbr = office.OfficeAbbr,
                    //Employee
                    EmpName = office.EmpName,
                    SwipeID = office.SwipeID,
                    eid = office.eid,
                    Position = office.Position,
                    SG = office.SG,
                    Status = office.Status,
                    isactive = office.isactive
                }).ToList();

            return mappedEmployee;
        }
    }
}
