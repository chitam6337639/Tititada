using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
		[Authorize(Roles = "Admin")]
		public IActionResult GetAll()
		{
			var products = _productService.GetAllProducts();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(Guid id)
		{
			var product = _productService.GetProductById(id);
			if (product == null)
			{
				return NotFound(new { Message = "Product not found" });
			}
			return Ok(product);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,User")]
		public IActionResult CreateProduct(ProductVM productVM)
		{
			var product = _productService.CreateProduct(productVM);
			return Ok(new
			{
				Success = true,
				Data = product
			});
		}

		[HttpPut("{id}")]
		public IActionResult UpdateProduct(Guid id,ProductVM productVM)
		{
			var existProduct = _productService.GetProductById(id);
			if (existProduct == null)
			{
				return NotFound(new { Message = "Product not found" });
			}

			_productService.UpdateProduct(id, productVM);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(Guid id)
		{
			var existProduct = _productService.GetProductById(id);
			if (existProduct == null)
			{
				return NotFound(new { Message = "Product not found" });
			}

			_productService.DeleteProduct(id);
			return Ok(new
			{
				Success = true,
				Message = "Success"
			});
		}
	}
}
