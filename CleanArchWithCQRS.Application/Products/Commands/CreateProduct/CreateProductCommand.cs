using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.CreateProduct
{
	public class CreateProductCommand : IRequest<ProductVM>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
	}
}
