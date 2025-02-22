using Microsoft.AspNetCore.Mvc;
using Shelter.Application.Services;
using Shelter.Shared.DTOs;

namespace Shelter.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] VolunteerDto userDto)
        {
            var result = await _authService.Register(userDto);
            if (!result)
                return BadRequest("El usuario ya existe.");

            return Ok("Usuario registrado exitosamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] VolunteerDto userDto)
        {
            var token = await _authService.Authenticate(userDto);
            if (token == null)
                return Unauthorized("Credenciales incorrectas.");

            return Ok(new { Token = token });
        }
    }
}
