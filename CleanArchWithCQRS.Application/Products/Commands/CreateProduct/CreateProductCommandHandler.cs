using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Commands.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVM>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<ProductVM> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var productEntity = new Product()
			{
				Name = request.Name,
				Description = request.Description,
				Price = request.Price

			};
			var Result = await _productRepository.CreateAsync(productEntity);
			return _mapper.Map<ProductVM>(Result);
		}
	}
}
