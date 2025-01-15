using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.DeleteProduct
{
	public class DeleteProductCommand : IRequest<int>
	{
		public int Id { get; set; }
	}
}
