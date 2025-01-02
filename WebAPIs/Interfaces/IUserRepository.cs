using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IUserRepository
	{
		User Authentication (string username, string password);
	}
}
