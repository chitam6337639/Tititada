using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Application.Common.Interfaces
{
	public interface ITokenGenerator
	{
		public string GenerateJWTToken((string userId, string userName, IList<string> roles) userDetails);
	}
}
