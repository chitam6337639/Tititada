using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Infrastructure.Catching
{
	public  interface ICacheService
	{
		Task<T?> GetCacheAsync<T>(string key);
		Task SetCacheAsync<T>(string key, T value, TimeSpan? expirationTime = null);
		Task RemoveCacheAsync(string key);
	}
}
