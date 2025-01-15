using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using CleanArchWithCQRS.Infrastructure.Catching;
using CleanArchWithCQRS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CleanArchWithCQRS.Infrastructure.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ICacheService _cacheService;
		private const string CacheKey = "Product";

		public ProductRepository(AppDbContext appDbContext, ICacheService cacheService)
		{
			_appDbContext = appDbContext;
			_cacheService = cacheService;
		}
		public async Task<List<Product>> GetAllProductAsync()
		{
			//return await _appDbContext.Products.ToListAsync();
			var cachedProducts = await _cacheService.GetCacheAsync<List<Product>>(CacheKey);
			if (cachedProducts != null)
			{
				return cachedProducts;
			}

			var products = await _appDbContext.Products.ToListAsync();

			await _cacheService.SetCacheAsync(CacheKey, products, TimeSpan.FromMinutes(5));

			return products;
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			var cacheKey = $"{CacheKey}_{id}";
			var cachedProduct = await _cacheService.GetCacheAsync<Product>(cacheKey);

			if (cachedProduct != null)
			{
				return cachedProduct;
			}

			var product = await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

			if (product != null)
			{
				await _cacheService.SetCacheAsync(cacheKey, product, TimeSpan.FromMinutes(5));
			}

			return product;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			await _appDbContext.Products.AddAsync(product);
			await _appDbContext.SaveChangesAsync();

			await _cacheService.RemoveCacheAsync(CacheKey);
			return product;
		}

		public async Task<int> UpdateAsync(int id, Product product)
		{
			var result = await _appDbContext.Products
				.Where(model => model.Id == id)
				.ExecuteUpdateAsync(setters => setters
					.SetProperty(m => m.Id, product.Id)
					.SetProperty(m => m.Name, product.Name)
					.SetProperty(m => m.Description, product.Description)
					.SetProperty(m => m.Price, product.Price));

			await _cacheService.RemoveCacheAsync($"{CacheKey}_{id}");
			await _cacheService.RemoveCacheAsync(CacheKey);
			return result;
		}

		public async Task<int> DeleteAsync(int id)
		{
			var result = await _appDbContext.Products.Where(model => model.Id == id).ExecuteDeleteAsync();

			await _cacheService.RemoveCacheAsync($"{CacheKey}_{id}");
			await _cacheService.RemoveCacheAsync(CacheKey);
			return result;
		}
	}
}
