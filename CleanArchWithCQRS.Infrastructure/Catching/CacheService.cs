using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchWithCQRS.Infrastructure.Catching
{
	public class CacheService : ICacheService
	{
		private readonly IDistributedCache _cache;

		public CacheService(IDistributedCache cache)
		{
			_cache = cache;
		}

		public async Task<T?> GetCacheAsync<T>(string key)
		{
			var cachedData = await _cache.GetStringAsync(key);
			return string.IsNullOrEmpty(cachedData) ? default : JsonConvert.DeserializeObject<T>(cachedData);
		}

		public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expirationTime = null)
		{
			var cacheOptions = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromMinutes(5)
			};
			var jsonData = JsonConvert.SerializeObject(value);
			await _cache.SetStringAsync(key, jsonData, cacheOptions);
		}

		public async Task RemoveCacheAsync(string key)
		{
			await _cache.RemoveAsync(key);
		}
	}
}
