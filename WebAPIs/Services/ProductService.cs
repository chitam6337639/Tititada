using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Services
{
	public class ProductService : IProductService
	{

		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public IEnumerable<Product> GetAllProducts()
		{
			return _productRepository.GetAll();
		}

		public Product CreateProduct(ProductVM productVM)
		{
			var product = new Product
			{
				ProductID = Guid.NewGuid(),
				ProductName = productVM.ProductName,
				Price = productVM.Price
			};
			_productRepository.Add(product);
			return product;
		}
	}
}
