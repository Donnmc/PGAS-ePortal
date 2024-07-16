using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.DTO.PMIS.View;

namespace PGAS.WebAPI.Controllers.PMIS.View
{
    [Route("api/[controller]")]
    [ApiController]
    public class vw_pgas_employeesDTOController : ControllerBase
    {
        private readonly pmisContext _context;

        public vw_pgas_employeesDTOController(pmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<vw_pgas_employeesDTO>>> GetEmployees()
        {
            var employee = await _context.vw_pgas_employees.ToListAsync();

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
                .Select(e => new vw_pgas_employeesDTO
                {
                    //Office
                    OfficeName = e.OfficeName,
                    OfficeAbbr = e.OfficeAbbr,
                    //Employee
                    EmployeeName = e.EmployeeName,
                    SwipeID = e.SwipeID,
                    eid = e.eid,
                    Position = e.Position,
                    SG = e.SG,
                    Status = e.Status,
                    isactive = e.isactive,
                    Telephone = e.Telephone,
                    EmailAdd = e.EmailAdd,
                    Cause = e.Cause,
                    AppointCoverage = e.AppointCoverage

                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("promotedEmployees")]
        public async Task<ActionResult<IEnumerable<vw_pgas_employeesDTO>>> GetPromotedEmployees()
        {
            var employee = await _context.vw_pgas_employees.ToListAsync();

            var mappedEmployee = employee
                .Where(e => e.Cause == "Promotion")
                .OrderByDescending(e => e.AppointCoverage)
                .Select(office => new vw_pgas_employeesDTO
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
                    isactive = office.isactive,
                    Telephone = office.Telephone,
                    EmailAdd = office.EmailAdd,
                    Cause = office.Cause,
                    AppointCoverage = office.AppointCoverage,

                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("query")]
        public async Task<ActionResult<IEnumerable<vw_pgas_employeesDTO>>> SearchEmployees(
        [FromQuery] string? searchDetails = null)
        {
            if (string.IsNullOrWhiteSpace(searchDetails))
            {
                return BadRequest("Search details cannot be null or empty.");
            }

            var query = _context.vw_pgas_employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchDetails))
            {
                query = query.Where(e =>
                    (e.OfficeName!.Contains(searchDetails)) ||
                    (e.OfficeAbbr!.Contains(searchDetails)) ||
                    (e.Position!.Contains(searchDetails)) ||
                    (e.EmployeeName!.Contains(searchDetails)) ||
                    (e.SwipeID!.Contains(searchDetails)) ||
                    (e.eid!.ToString().Contains(searchDetails)));
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
                .Select(e => new vw_pgas_employeesDTO
                {
                    //Office
                    OfficeName = e.OfficeName,
                    OfficeAbbr = e.OfficeAbbr,
                    //Employee
                    EmployeeName = e.EmployeeName,
                    SwipeID = e.SwipeID,
                    eid = e.eid,
                    Position = e.Position,
                    SG = e.SG,
                    Status = e.Status,
                    isactive = e.isactive,
                    Telephone = e.Telephone,
                    EmailAdd = e.EmailAdd,
                    Cause = e.Cause,
                    AppointCoverage = e.AppointCoverage
                }).ToList();

            return mappedEmployee;
        }

        [HttpGet("id/{eid}")]
        public async Task<ActionResult<vw_pgas_employeesDTO>> GetEmployeeById(long eid)
        {
            var employee = await _context.vw_pgas_employees
                .Where(e => e.eid == eid)
                .Select(e => new vw_pgas_employeesDTO
                {
                    //Office
                    OfficeName = e.OfficeName,
                    OfficeAbbr = e.OfficeAbbr,
                    //Employee
                    EmployeeName = e.EmployeeName,
                    SwipeID = e.SwipeID,
                    eid = e.eid,
                    Position = e.Position,
                    SG = e.SG,
                    Status = e.Status,
                    isactive = e.isactive,
                    Telephone = e.Telephone,
                    EmailAdd = e.EmailAdd,
                    Cause = e.Cause,
                    AppointCoverage = e.AppointCoverage
                })
                .AsNoTracking().FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("name/{EmployeeName}")]
        public async Task<ActionResult<vw_pgas_employeesNamesDTO>> GetEmployeeByNameAsync(string EmployeeName)
        {
            var employee = await _context.vw_pgas_employees
                .Where(e => e.EmployeeName == EmployeeName)
                .Select(e => new vw_pgas_employeesNamesDTO
                {
                    EmployeeName = e.EmployeeName
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
