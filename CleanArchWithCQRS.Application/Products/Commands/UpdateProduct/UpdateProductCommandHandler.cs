using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.UpdateProduct
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
	{
		private readonly IProductRepository _productRepository;
		public UpdateProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var UpdateProductEntity = new Product()
			{
				Id = request.Id,
				Name = request.Name,
				Description = request.Description,
				Price = request.Price
			};
			
			return await _productRepository.UpdateAsync(request.Id, UpdateProductEntity);

		}
	}
}
