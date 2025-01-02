using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly List<User> _users = new()
		{
			new User { Username = "admin", Password = "1234", Role = "Admin" },
			new User { Username = "user", Password = "1234", Role = "User" }
		};

		public User Authentication(string username, string password)
		{
			return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
		}
	}
}
