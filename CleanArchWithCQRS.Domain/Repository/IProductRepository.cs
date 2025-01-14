using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Entities;

namespace CleanArchWithCQRS.Domain.Repository
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllProductAsync();
		Task<Product> GetByIdAsync(int id);
		Task<Product> CreateAsync(Product product);
		Task<int> UpdateAsync(int id, Product product);
		Task<int> DeleteAsync(int id);
	}
}
