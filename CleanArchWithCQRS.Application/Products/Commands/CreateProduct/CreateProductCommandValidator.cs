using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchWithCQRS.Application.Products.Commands.CreateProduct
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator() 
		{
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required")
				.MaximumLength(200).WithMessage("Name must not exceed 200 characters");

			RuleFor(v => v.Description)
				.NotEmpty().WithMessage("Description is required")
				.MaximumLength(200).WithMessage("Description must not exceed 200 characters");

			RuleFor(v => v.Price)
				.NotEmpty().WithMessage("Price is required");
		}
	}
}
