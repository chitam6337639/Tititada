﻿using CleanArchWithCQRS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Application.Role.Commands.UpdateRole
{
	public class UpdateRoleCommand : IRequest<int>
	{
		public string Id { get; set; }
		public string RoleName { get; set; }
	}

	public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, int>
	{
		private readonly IIdentityService _identityService;

		public UpdateRoleCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.UpdateRole(request.Id, request.RoleName);
			return result ? 1 : 0;
		}
	}
}
