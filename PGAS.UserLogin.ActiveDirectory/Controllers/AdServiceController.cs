using ADIntegratedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ADIntegratedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AdService _adService;

        public UserController(AdService adService)
        {
            _adService = adService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string username, [DataType(DataType.Password)][FromQuery] string password)
        {
            var isAuthenticated = _adService.AuthenticateUser(username, password);
            if (isAuthenticated)
            {
                var roles = _adService.GetUserRoles(username);
                if (roles.Any())
                {
                    return Ok(new { Message = "User authenticated", Roles = roles });
                }
                else
                {
                    return NotFound(new { Message = "User has no roles assigned" });
                }
            }
            else
            {
                return Unauthorized(new { Message = "Authentication failed" });
            }
        }
    }
}
