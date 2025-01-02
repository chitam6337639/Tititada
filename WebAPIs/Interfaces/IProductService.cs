using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IProductService
	{
		IEnumerable<Product> GetAllProducts();
		Product GetProductById(Guid id);
		Product CreateProduct(ProductVM  productVM);
		void UpdateProduct(Guid id, ProductVM productVM);
		void DeleteProduct(Guid id);
	}
}
