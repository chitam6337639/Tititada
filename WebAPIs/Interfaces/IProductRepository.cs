using System.Numerics;
using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAll();
		void Add(Product product);
	}
}
