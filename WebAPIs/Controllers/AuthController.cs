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
		public IActionResult Login([FromBody] LoginVM login)
		{
			var token = _authService.Authenticate(login.Username, login.Password);

			if (string.IsNullOrEmpty(token))
			{
				return Unauthorized(new { Message = "Invalid username or password" });
			}

			return Ok(new { Token = token });
		}
	}
}
