using ECommerceAPI.Application.Features.SubCategories.Command.DeleteSubCategory;
using FluentValidation;

public class DeleteSubCategoryCommandValidator : AbstractValidator<DeleteSubCategoryCommandRequest>
{
    public DeleteSubCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("SubCategory Id must be greater than 0.");
    }
}
