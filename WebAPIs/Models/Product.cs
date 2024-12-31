namespace WebAPIs.Models
{
	public class ProductVM
	{
		public string ProductName { get; set; }
		public double Price { get; set; }
	}

	public class Product : ProductVM
	{
		public Guid ProductID { get; set; }
	}

}
