using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchWithCQRS.Application.Products.Commands.DeleteProduct;
using CleanArchWithCQRS.Domain.Repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace CleanArchWithCQRS.Application.UnitTests.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly DeleteProductCommandHandler _handler;

		public DeleteProductCommandHandlerTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
			_handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnDeletedProductId_WhenProductExists()
		{
			// Arrange
			var productId = 1;
			_productRepositoryMock
				.Setup(repo => repo.DeleteAsync(productId))
				.ReturnsAsync(productId);

			var command = new DeleteProductCommand { Id = productId };

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.Should().Be(productId);
			_productRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenProductDoesNotExist()
		{
			// Arrange
			var nonExistentProductId = 999;
			_productRepositoryMock
				.Setup(repo => repo.DeleteAsync(nonExistentProductId))
				.ThrowsAsync(new KeyNotFoundException("Product not found"));

			var command = new DeleteProductCommand { Id = nonExistentProductId };

			// Act
			var act = async () => await _handler.Handle(command, CancellationToken.None);

			// Assert
			await act.Should().ThrowAsync<KeyNotFoundException>()
				.WithMessage("Product not found");

			_productRepositoryMock.Verify(repo => repo.DeleteAsync(nonExistentProductId), Times.Once);
		}
	}
}
