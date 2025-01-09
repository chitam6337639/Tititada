using Microsoft.AspNetCore.Mvc;
using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Controllers
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

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginVM login)
		{
			if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
			{
				return BadRequest(new { Message = "Username and password are required" });
			}

			var token = await _authService.AuthenticateAsync(login.Username, login.Password);

			if (string.IsNullOrEmpty(token))
			{
				return Unauthorized(new { Message = "Invalid username or password" });
			}

			return Ok(new { Token = token });
		}
	}
}
