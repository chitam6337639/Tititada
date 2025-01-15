using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Queries.GetProducts
{
	public class GetProductQuery : IRequest<List<ProductVM>>
	{
	}
	//public record GetBlogQuery : IRequest<List<ProductVM>>
}
