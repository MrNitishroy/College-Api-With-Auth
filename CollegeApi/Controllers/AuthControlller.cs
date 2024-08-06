using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // This endpoint checks the authentication status.
        [Authorize]
        [HttpGet("status", Name = "AuthStatus")]
        public ActionResult AuthStatus()
        {
            return Ok(new { Message = "Authenticated", User = User.Identity.Name });
        }

        // Optionally, you can add a public endpoint to test access without authentication.
        [AllowAnonymous]
        [HttpGet("public")]
        public ActionResult PublicEndpoint()
        {
            return Ok(new { Message = "This is a public endpoint accessible without authentication." });
        }
    }
}
