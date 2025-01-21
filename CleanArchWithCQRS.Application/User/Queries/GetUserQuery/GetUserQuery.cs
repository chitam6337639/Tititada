﻿using CleanArchWithCQRS.Application.Common.Interfaces;
using CleanArchWithCQRS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Application.User.Queries.GetUserQuery
{
	public class GetUserQuery : IRequest<List<UserResponseDTO>>
	{
	}

	public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserResponseDTO>>
	{
		private readonly IIdentityService _identityService;

		public GetUserQueryHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public async Task<List<UserResponseDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			var users = await _identityService.GetAllUsersAsync();
			return users.Select(x => new UserResponseDTO()
			{
				Id = x.id,
				FullName = x.fullName,
				UserName = x.userName,
				Email = x.email
			}).ToList();
		}
	}
}
