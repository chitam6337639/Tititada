using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWithCQRS.Application.Products.Commands.CreateProduct;
using CleanArchWithCQRS.Application.Products.Queries.GetProducts;
using CleanArchWithCQRS.Domain.Entities;
using CleanArchWithCQRS.Domain.Repository;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CleanArchWithCQRS.Application.UnitTests.Products.Commands.CreateProduct
{
	public class CreateProductCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly CreateProductCommandHandler _handler;
		private readonly CreateProductCommandValidator _validator;

		public CreateProductCommandHandlerTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
			_mapperMock = new Mock<IMapper>();
			_handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _mapperMock.Object);
			_validator = new CreateProductCommandValidator();
		}

		[Fact]
		public async Task Handle_Should_CreateProduct_And_ReturnProductVM()
		{
			// Arrange
			var createCommand = new CreateProductCommand
			{
				Name = "Macbook",
				Description = "M1",
				Price = "1000$"
			};

			var productEntity = new Product
			{
				Id = 1,
				Name = createCommand.Name,
				Description = createCommand.Description,
				Price = createCommand.Price
			};

			var productVM = new ProductVM
			{
				Id = productEntity.Id,
				Name = productEntity.Name,
				Description = productEntity.Description,
				Price = productEntity.Price
			};

			_productRepositoryMock
				.Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
				.ReturnsAsync(productEntity);

			_mapperMock
				.Setup(mapper => mapper.Map<ProductVM>(It.IsAny<Product>()))
				.Returns(productVM);

			// Act
			var result = await _handler.Handle(createCommand, CancellationToken.None);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(1);
			result.Name.Should().Be(createCommand.Name);
			result.Description.Should().Be(createCommand.Description);
			result.Price.Should().Be(createCommand.Price);

			_productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>()), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ProductVM>(It.IsAny<Product>()), Times.Once);
		}
		[Fact]
		public async Task Handle_Should_ThrowException_When_RepositoryFails()
		{
			// Arrange
			var createCommand = new CreateProductCommand
			{
				Name = "Invalid Product",
				Description = "Invalid Description",
				Price = "50"
			};

			_productRepositoryMock
				.Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
				.ThrowsAsync(new Exception("Database error"));

			// Act
			Func<Task> act = async () => await _handler.Handle(createCommand, CancellationToken.None);

			// Assert
			await act.Should().ThrowAsync<Exception>()
				.WithMessage("Database error"); 

			_productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>()), Times.Once);
			_mapperMock.Verify(mapper => mapper.Map<ProductVM>(It.IsAny<Product>()), Times.Never);
		}
		[Fact]
		public void Validate_ShouldHaveError_WhenNameIsEmpty()
		{
			// Arrange
			var command = new CreateProductCommand
			{
				Name = "",
				Description = "m1",
				Price = "1000"
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldHaveValidationErrorFor(c => c.Name)
				.WithErrorMessage("Name is required");
		}
		[Fact]
		public void Validate_ShouldHaveError_WhenDescriptionIsEmpty()
		{
			// Arrange
			var command = new CreateProductCommand
			{
				Name = "Mac",
				Description = "",
				Price = "1000"
			};

			// Act
			var result = _validator.TestValidate(command);

			// Assert
			result.ShouldHaveValidationErrorFor(c => c.Description)
				.WithErrorMessage("Description is required");
		}

	}
}
