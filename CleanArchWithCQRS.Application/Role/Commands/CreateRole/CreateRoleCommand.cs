using CleanArchWithCQRS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Application.Role.Commands.CreateRole
{
	public class CreateRoleCommand : IRequest<int>
	{
		public string RoleName { get; set; }
	}

	public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
	{
		private readonly IIdentityService _identityService;

		public CreateRoleCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _identityService.CreateRoleAsync(request.RoleName);
			return result ? 1 : 0;
		}
	}
}
