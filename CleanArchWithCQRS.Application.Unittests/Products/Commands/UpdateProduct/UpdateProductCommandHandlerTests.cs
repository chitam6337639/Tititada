using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Application.Products.Commands.UpdateProduct;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchWithCQRS.Application.UnitTests.Products.Commands.UpdateProduct
{
	public class UpdateProductCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly UpdateProductCommandHandler _handler;

		public UpdateProductCommandHandlerTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
			_handler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnUpdatedProductId_WhenProductIsUpdatedSuccessfully()
		{
			// Arrange
			var productId = 1;
			var updateProductCommand = new UpdateProductCommand
			{
				Id = productId,
				Name = "Mac1",
				Description = "m2",
				Price = "1000"
			};

			var updatedProduct = new Product
			{
				Id = productId,
				Name = "Mac1",
				Description = "m2",
				Price = "1000"
			};

			_productRepositoryMock
				.Setup(repo => repo.UpdateAsync(productId, It.IsAny<Product>()))
				.ReturnsAsync(productId);

			// Act
			var result = await _handler.Handle(updateProductCommand, CancellationToken.None);

			// Assert
			result.Should().Be(productId);
			_productRepositoryMock.Verify(
				repo => repo.UpdateAsync(productId, It.Is<Product>(p =>
					p.Id == productId &&
					p.Name == updateProductCommand.Name &&
					p.Description == updateProductCommand.Description &&
					p.Price == updateProductCommand.Price
				)),
				Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenProductToUpdateDoesNotExist()
		{
			// Arrange
			var nonExistentProductId = 999;
			var updateProductCommand = new UpdateProductCommand
			{
				Id = nonExistentProductId,
				Name = "Mac1",
				Description = "m2",
				Price = "100.00"
			};

			_productRepositoryMock
				.Setup(repo => repo.UpdateAsync(nonExistentProductId, It.IsAny<Product>()))
				.ThrowsAsync(new KeyNotFoundException("Product not found"));

			// Act
			var act = async () => await _handler.Handle(updateProductCommand, CancellationToken.None);

			// Assert
			await act.Should().ThrowAsync<KeyNotFoundException>()
				.WithMessage("Product not found");

			_productRepositoryMock.Verify(
				repo => repo.UpdateAsync(nonExistentProductId, It.IsAny<Product>()),
				Times.Once);
		}
	}
}
