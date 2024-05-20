using ePortal.WebAPI.Context;
using ePortal.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers.PMIS
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
        public async Task<ActionResult<eportalUser>> Login(string username, string password)
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
