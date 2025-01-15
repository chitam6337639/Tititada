using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Repository;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
	{
		private readonly IProductRepository _productRepository;

		public DeleteProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			return await _productRepository.DeleteAsync(request.Id);
		}
	}
}
