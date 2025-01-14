using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using CleanArchWithCQRS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchWithCQRS.Infrastructure.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _appDbContext;
		public ProductRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<List<Product>> GetAllProductAsync()
		{
			return await _appDbContext.Products.ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			return await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Product> CreateAsync(Product product)
		{
			await _appDbContext.Products.AddAsync(product);
			await _appDbContext.SaveChangesAsync();
			return product;
		}

		public async Task<int> UpdateAsync(int id, Product product)
		{
			return await _appDbContext.Products
				.Where(model => model.Id == id)
				.ExecuteUpdateAsync(setters => setters
					.SetProperty(m => m.Id, product.Id)
					.SetProperty(m => m.Name, product.Name)
					.SetProperty(m => m.Description, product.Description)
					.SetProperty(m => m.Price, product.Price));
		}

		public async Task<int> DeleteAsync(int id)
		{
			return await _appDbContext.Products.Where(model => model.Id == id).ExecuteDeleteAsync();
		}
	}
}
