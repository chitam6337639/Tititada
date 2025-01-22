using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using CleanArchWithCQRS.Domain.Repository;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Queries.GetProductById
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVM>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}

		public async Task<ProductVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.GetByIdAsync(request.ProductId);
			if (product == null)
			{
				return null;
			}
			return _mapper.Map<ProductVM>(product);
		}

	}
}
