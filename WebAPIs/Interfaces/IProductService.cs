using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IProductService
	{
		IEnumerable<Product> GetAllProducts();
		Product GetProductById(int id);
		Product CreateProduct(Product product);
		void UpdateProduct(int id, Product product);
		void DeleteProduct(int id);
	}
}
