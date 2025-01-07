using WebAPIs.Data;
using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Product> GetAll()
		{
			return _context.Products.ToList();
		}

		public Product GetById(int id)
		{
			return _context.Products.Find(id);
		}
		public void Add(Product product)
		{
			_context.Products.Add(product);
			_context.SaveChanges();
		}

		public void Update(Product product)
		{
			_context.Products.Update(product);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var product = GetById(id);
			if (product != null)
			{
				_context.Remove(product);
				_context.SaveChanges();
			}
		}
	}
}
