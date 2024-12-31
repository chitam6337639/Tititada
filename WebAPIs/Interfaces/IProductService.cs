using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IProductService
	{
		IEnumerable<Product> GetAllProducts();
		Product CreateProduct(ProductVM  productVM);
	}
}
