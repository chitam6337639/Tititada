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

		public Product GetProductById(int id)
		{
			return _productRepository.GetById(id);
		}

		public Product CreateProduct(Product product)
		{
			_productRepository.Add(product);
			return product;
		}

		public void UpdateProduct(int id, Product product)
		{
			var existProduct = _productRepository.GetById(product.ProductID);
			if (existProduct != null)
			{
				existProduct.ProductName = product.ProductName;
				existProduct.Price = product.Price;

				_productRepository.Update(existProduct);
			}
		}

		public void DeleteProduct(int id)
		{
			_productRepository.Delete(id);
		}
	}
}
