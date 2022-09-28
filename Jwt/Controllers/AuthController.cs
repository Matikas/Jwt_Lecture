using Jwt.Dto;
using Jwt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup(SignupDto signupModel)
        {
            await _authService.SignupNewAccountAsync(signupModel.Username, signupModel.Password);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginModel)
        {
            var loginSuccess = await _authService.LoginAsync(loginModel.Username, loginModel.Password);

            if (!loginSuccess)
            {
                return BadRequest();
            }

            return Ok(_jwtService.GetJwtToken(loginModel.Username));
        }
    }
}
