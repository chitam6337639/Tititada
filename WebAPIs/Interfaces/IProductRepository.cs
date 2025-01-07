using System.Numerics;
using WebAPIs.Models;

namespace WebAPIs.Interfaces
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAll();
		Product GetById(int id);
		void Add(Product product);
		void Update(Product product);
		void Delete(int id);
	}
}
