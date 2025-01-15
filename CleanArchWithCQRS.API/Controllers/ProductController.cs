using CleanArchWithCQRS.Application.Products.Commands.CreateProduct;
using CleanArchWithCQRS.Application.Products.Commands.DeleteProduct;
using CleanArchWithCQRS.Application.Products.Commands.UpdateProduct;
using CleanArchWithCQRS.Application.Products.Queries.GetProductById;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchWithCQRS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ApiControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var products = await Mediator.Send(new GetProductQuery());
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await Mediator.Send(new GetProductByIdQuery() { ProductId = id });
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommand command)
		{
			var createdProduct = await Mediator.Send(command);
			return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateProductCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}

			await Mediator.Send(command);
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await Mediator.Send(new DeleteProductCommand{ Id = id });
			if (result == 0)
			{
				return BadRequest(string.Empty);
			}
			return NoContent();
		}

	}
}
