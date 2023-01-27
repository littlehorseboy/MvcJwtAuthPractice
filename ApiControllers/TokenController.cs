using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcJwtAuthPractice.Helpers;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcJwtAuthPractice.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers _jwtHelpers;

        public TokenController(JwtHelpers jwtHelpers)
        {
            this._jwtHelpers = jwtHelpers;
        }

        [HttpPost("signin")]
        public ActionResult<string> Signin(LoginDto body)
        {
            if (body.Password != "haha")
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            var token = _jwtHelpers.generateToken(body.UserName);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("getClaims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize]
        [HttpGet("getUserName")]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

    }
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
