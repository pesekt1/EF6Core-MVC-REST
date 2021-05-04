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
        public async Task<IActionResult> login(string username, string password)
        {
            var userDetails =  await _authService.Login(username, password);
            if (userDetails == null)
            {
                //ModelState.AddModelError("Password", "Invalid login attempt.");
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "error occurred, incorrect user name or password" });
            }
            //HttpContext.Session.SetString("userId", customerDetails.Firstname);
            return Ok(new AuthSuccessResponse { 
                Token = userDetails.Token
            });
        }
    }
}