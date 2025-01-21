using CleanArchWithCQRS.Application.Common.Interfaces;
using CleanArchWithCQRS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Application.Role.Queries
{
	public class GetRoleQuery : IRequest<IList<RoleResponseDTO>>
	{

	}

	public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IList<RoleResponseDTO>>
	{
		private readonly IIdentityService _identityService;

		public GetRoleQueryHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public async Task<IList<RoleResponseDTO>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
		{
			var roles = await _identityService.GetRolesAsync();
			return roles.Select(role => new RoleResponseDTO() { Id = role.id, RoleName = role.roleName }).ToList();
		}
	}
}
