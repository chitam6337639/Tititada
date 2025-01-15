using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Domain.Repository;
using MediatR;

namespace CleanArchWithCQRS.Application.Products.Queries.GetProducts
{
	public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductVM>>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}
		public async Task<List<ProductVM>> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var products = await _productRepository.GetAllProductAsync();
			//var productList = products.Select(x => new ProductVM
			//{
			//	Description = x.Description, Name = x.Name, Price = x.Price, Id = x.Id
			//}).ToList();

			var productList = _mapper.Map<List<ProductVM>>(products);
			return productList;
		}
	}
}
