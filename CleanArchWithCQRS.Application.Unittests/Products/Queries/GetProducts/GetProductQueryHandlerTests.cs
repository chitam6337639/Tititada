using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using Moq;
using Xunit;
using Assert = NUnit.Framework.Assert;


namespace CleanArchWithCQRS.Application.UnitTests.Products.Queries.GetProducts
{
	public class GetProductQueryHandlerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly GetProductQueryHandler _handler;

		public GetProductQueryHandlerTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
			_mapperMock = new Mock<IMapper>();
			_handler = new GetProductQueryHandler(_productRepositoryMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnProductVMList_WhenProductsExist()
		{
			// Arrange
			var productList = new List<Product>
			{
				new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = "100" },
				new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = "200" }
			};

			var productVMList = new List<ProductVM>
			{
				new ProductVM { Id = 1, Name = "Product 1", Description = "Description 1", Price = "100" },
				new ProductVM { Id = 2, Name = "Product 2", Description = "Description 2", Price = "200" }
			};

			_productRepositoryMock
				.Setup(repo => repo.GetAllProductAsync())
				.ReturnsAsync(productList);

			_mapperMock
				.Setup(mapper => mapper.Map<List<ProductVM>>(productList))
				.Returns(productVMList);

			var query = new GetProductQuery();

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.AreEqual(2, result.Count); 
			Assert.AreEqual("Product 1", result[0].Name); 
			Assert.AreEqual("Product 2", result[1].Name); 


			_productRepositoryMock.Verify(repo => repo.GetAllProductAsync(), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<List<ProductVM>>(productList), Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
		{
			// Arrange
			var productList = new List<Product>(); 

			_productRepositoryMock
				.Setup(repo => repo.GetAllProductAsync())
				.ReturnsAsync(productList);

			_mapperMock
				.Setup(mapper => mapper.Map<List<ProductVM>>(productList))
				.Returns(new List<ProductVM>());

			var query = new GetProductQuery();

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.IsEmpty(result); 

			_productRepositoryMock.Verify(repo => repo.GetAllProductAsync(), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<List<ProductVM>>(productList), Times.Once);
		}
	}
}
