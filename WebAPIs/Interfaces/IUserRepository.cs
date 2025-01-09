using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IUserRepository
	{
		Task<User> AuthenticateAsync(string username, string password);
	}
}
