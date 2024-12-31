using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Interfaces;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			var products = _productService.GetAllProducts();
			return Ok(products);
		}

		[HttpPost]
		public IActionResult CreateProduct(ProductVM productVM)
		{
			var product = _productService.CreateProduct(productVM);
			return Ok(new
			{
				Success = true,
				Data = product
			});
		}
	}
}
