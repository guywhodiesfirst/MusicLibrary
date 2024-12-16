using Business.Interfaces;
using Business.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            Console.WriteLine("hello");
            try
            {
                var result = await _authService.Login(request);
                return Ok(new {success = true, data = result});
            }
            catch (Exception ex)
            {
                return Unauthorized(new {success = false, message = ex.Message});
            }
        }

        // POST: api/auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            try
            {
                await _authService.Register(model);
                return Ok(new {success = true, message = "Registration successful!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new {success = false, message = ex.Message});
            }
        }
    }
}