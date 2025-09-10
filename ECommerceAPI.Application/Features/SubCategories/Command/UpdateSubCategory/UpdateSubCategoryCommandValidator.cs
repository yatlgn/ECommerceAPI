using ECommerceAPI.Application.Features.SubCategories.Command.UpdateSubCategory;
using FluentValidation;

public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommandRequest>
{
    public UpdateSubCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("SubCategory Id must be greater than 0.");

        RuleFor(x => x.SubCategoryName)
            .NotEmpty().WithMessage("SubCategory name is required.")
            .MaximumLength(100).WithMessage("SubCategory name must not exceed 100 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
    }
}
