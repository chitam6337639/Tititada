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
		//[Authorize(Roles = "Admin")]
		public IActionResult GetAll()
		{
			var products = _productService.GetAllProducts();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var product = _productService.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		//[Authorize(Roles = "Admin,User")]
		public IActionResult CreateProduct([FromBody]Product product)
		{
			var createdProduct = _productService.CreateProduct(product);
			return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductID }, createdProduct);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateProduct(int id, [FromBody]Product product)
		{
			var existProduct = _productService.GetProductById(id);
			if (existProduct == null)
			{
				return NotFound(new { Message = "Product not found" });
			}

			product.ProductID = id; 
			_productService.UpdateProduct(id,product);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
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
