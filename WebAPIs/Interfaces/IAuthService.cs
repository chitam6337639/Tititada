namespace WebAPIs.Interfaces
{
	public interface IAuthService
	{
		Task<string> AuthenticateAsync(string username, string password);
		bool ValidateToken(string token);
		string GetUsernameFromToken(string token);
	}
}
