using ECommerceAPI.Application.Features.Products.Command.CreateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");

            RuleFor(x => x.SubCategoryId)
                .GreaterThan(0).WithMessage("SubCategoryId must be greater than zero.");

            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }
    }
}
