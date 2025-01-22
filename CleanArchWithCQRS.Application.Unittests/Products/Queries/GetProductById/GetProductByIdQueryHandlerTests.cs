using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Products.Queries.GetProductById;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchWithCQRS.Application.UnitTests.Products.Queries.GetProductById
{
	public class GetProductByIdQueryHandlerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly GetProductByIdQueryHandler _handler;

		public GetProductByIdQueryHandlerTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
			_mapperMock = new Mock<IMapper>();
			_handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnProductVM_WhenProductExists()
		{
			// Arrange
			var productId = 1;
			var product = new Product
			{
				Id = productId,
				Name = "Mac1",
				Description = "m2",
				Price = "100.00"
			};

			var productVm = new ProductVM
			{
				Id = productId,
				Name = "Mac1",
				Description = "m2",
				Price = "100.00"
			};

			_productRepositoryMock
				.Setup(repo => repo.GetByIdAsync(productId))
				.ReturnsAsync(product);

			_mapperMock
				.Setup(mapper => mapper.Map<ProductVM>(product))
				.Returns(productVm);

			var query = new GetProductByIdQuery { ProductId = productId };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(productVm);

			_productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ProductVM>(product), Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldReturnNull_WhenProductDoesNotExist()
		{
			// Arrange
			var productId = 15;

			_productRepositoryMock
				.Setup(repo => repo.GetByIdAsync(productId))
				.ReturnsAsync((Product)null);

			var query = new GetProductByIdQuery { ProductId = productId };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.Should().BeNull();

			_productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ProductVM>(It.IsAny<Product>()), Times.Never);
		}
	}
}
