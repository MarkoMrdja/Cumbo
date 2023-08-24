using Cumbo.Server.Services.AuthService;
using Cumbo.Shared;
using Cumbo.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            if (ModelState.IsValid)
            {
                response = await _authService.Register(registrationDto);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }

            response.Success = false;
            response.Message = "Invalid payload";

            return BadRequest(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            if (ModelState.IsValid)
            {
                response = await _authService.Login(loginDto);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }

            response.Success = false;
            response.Message = "Invalid payload";

            return BadRequest(response);
        }
    }
}
