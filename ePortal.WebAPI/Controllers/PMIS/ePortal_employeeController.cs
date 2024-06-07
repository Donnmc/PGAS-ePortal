using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.PMIS
{
    [Route("api/[controller]")]
    [ApiController]
    public class ePortal_employeeController : ControllerBase
    {
        private readonly pmisContext _context;

        public ePortal_employeeController(pmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ePortal_employee>>> GetEmployees()
        {
            var employee = await _context.ePortal_employee.ToListAsync();

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
                .OrderBy(o => o.OfficeName) // Order by OfficeName
                .ThenBy(e =>  // Order statuses in the desired order (descending)
                    Array.IndexOf(statusOrder, e.Status) // Get index based on the order
                )
                .ThenByDescending(e => e.SG) // Order by SG (descending)
                .Select(office => new ePortal_employee
                {
                    //Office
                    OfficeName = office.OfficeName,
                    OfficeAbbr = office.OfficeAbbr,
                    //Employee
                    EmployeeName = office.EmployeeName,
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
        public async Task<ActionResult<IEnumerable<ePortal_employee>>> SearchEmployees(
        [FromQuery] string? searchDetails = null)
        {
            if (string.IsNullOrWhiteSpace(searchDetails))
            {
                return BadRequest("Search details cannot be null or empty.");
            }

            var query = _context.ePortal_employee.AsQueryable();

            if (!string.IsNullOrEmpty(searchDetails))
            {
                query = query.Where(e =>
                    e.OfficeName.Contains(searchDetails) ||
                    e.OfficeAbbr.Contains(searchDetails) ||
                    e.Position.Contains(searchDetails) ||
                    e.EmployeeName.Contains(searchDetails) ||
                    e.SwipeID.Contains(searchDetails) ||
                    e.eid.ToString().Contains(searchDetails));
            }

            var employee = await query.ToListAsync();

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
                .Select(office => new ePortal_employee
                {
                    //Office
                    OfficeName = office.OfficeName,
                    OfficeAbbr = office.OfficeAbbr,
                    //Employee
                    EmployeeName = office.EmployeeName,
                    SwipeID = office.SwipeID,
                    eid = office.eid,
                    Position = office.Position,
                    SG = office.SG,
                    Status = office.Status,
                    isactive = office.isactive
                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("id/{eid}")]
        public async Task<ActionResult<ePortal_employee>> GetEmployeeById(long eid)
        {
            var employee = await _context.ePortal_employee
                .Where(e => e.eid == eid)
                .Select(e => new ePortal_employee
                {
                    OfficeName = e.OfficeName,
                    OfficeAbbr = e.OfficeAbbr,
                    EmployeeName = e.EmployeeName,
                    eid = e.eid,
                    SwipeID = e.SwipeID,
                    Position = e.Position,
                    SG = e.SG,
                    Status = e.Status,
                    isactive = e.isactive
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("name/{EmployeeName}")]
        public async Task<ActionResult<ePortal_employee>> GetEmployeeByName(string EmployeeName)
        {
            var employee = await _context.ePortal_employee // Replace YourEmployeeTable with your actual table name
                .Where(e => e.EmployeeName == EmployeeName)
                .Select(e => new ePortal_employee
                {
                    OfficeName = e.OfficeName,
                    OfficeAbbr = e.OfficeAbbr,
                    EmployeeName = e.EmployeeName,
                    eid = e.eid,
                    SwipeID = e.SwipeID,
                    Position = e.Position,
                    SG = e.SG,
                    Status = e.Status,
                    isactive = e.isactive
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
    }
}
