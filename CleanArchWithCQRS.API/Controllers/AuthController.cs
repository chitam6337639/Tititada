﻿using CleanArchWithCQRS.Application.Auth.Commands;
using CleanArchWithCQRS.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchWithCQRS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpPost("Login")]
		[ProducesDefaultResponseType(typeof(AuthResponseDTO))]
		public async Task<IActionResult> Login([FromBody] AuthCommand command)
		{
			return Ok(await _mediator.Send(command));
		}
	}
}
