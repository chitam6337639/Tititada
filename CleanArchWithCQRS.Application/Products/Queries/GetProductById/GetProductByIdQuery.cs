using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Queries.GetProductById
{
	public class GetProductByIdQuery : IRequest<ProductVM>
	{
		public int ProductId { get; set; }

	}
}
