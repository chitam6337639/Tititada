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

		public Product GetById(Guid id)
		{
			return _products.FirstOrDefault(p => p.ProductID == id);
		}
		public void Add(Product product)
		{
			_products.Add(product);
		}

		public void Update(Product product)
		{
			var existProduct = GetById(product.ProductID);
			if (existProduct != null)
			{
				existProduct.ProductName = product.ProductName;
				existProduct.Price = product.Price;
			}
		}

		public void Delete(Guid id)
		{
			var product = GetById(id);
			if (product != null)
			{
				_products.Remove(product);
			}
		}
	}
}
