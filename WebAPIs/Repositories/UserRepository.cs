using Microsoft.EntityFrameworkCore;
using WebAPIs.Data;
using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<User> AuthenticateAsync(string username, string password)
		{
			return await _context.Users
				.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
		}
	}
}
