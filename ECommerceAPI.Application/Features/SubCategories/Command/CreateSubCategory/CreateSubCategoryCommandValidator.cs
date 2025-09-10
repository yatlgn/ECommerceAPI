using ECommerceAPI.Application.Features.SubCategories.Command.CreateSubCategory;
using FluentValidation;

public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommandRequest>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(x => x.SubCategoryName)
            .NotEmpty().WithMessage("SubCategory name is required.")
            .MaximumLength(100).WithMessage("SubCategory name must not exceed 100 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
    }
}
