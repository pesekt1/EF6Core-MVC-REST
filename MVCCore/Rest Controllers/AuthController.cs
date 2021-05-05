using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCCore.DbContext;
using MVCCore.Options;
using MVCCore.Services;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(SchoolContext context, AuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var authResult =  await _authService.Login(username, password);
            if (authResult == null)
            {
                //ModelState.AddModelError("Password", "Invalid login attempt.");
                //Status400BadRequest
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "error occurred, incorrect user name or password" });
            }
            //HttpContext.Session.SetString("userId", customerDetails.Firstname);
            return Ok(new AuthSuccessResponse { 
                Token = authResult.Token
            });
        }
    }
}