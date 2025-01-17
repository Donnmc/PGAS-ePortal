using PGAS.WebAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.db_pmis.pmis.Table;

namespace PGAS.WebAPI.Controllers.db_pmis.pmis.Table
{
    [Route("api/[controller]")]
    [ApiController]
    public class eportalUserController : ControllerBase
    {
        private readonly pmisContext _context;

        public eportalUserController(pmisContext context)
        {
            _context = context;
        }

        [HttpGet("login/{username}/{password}")]
        public async Task<ActionResult<eportalUserDTO>> Login(string username, string password)
        {
            var user = await _context.eportalUser
                .FirstOrDefaultAsync(u => u.username == username && u.passcode == password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(user);
        }
    }
}
