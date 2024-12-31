using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly List<Product> _products = new List<Product>();

		public IEnumerable<Product> GetAll()
		{
			return _products;
		}

		public void Add(Product product)
		{
			_products.Add(product);
		}
	}
}
