using ECommerceAPI.Application.Features.SubCategories.Command.CreateSubCategory;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateSubCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var subCategory = new SubCategory
        {
            SubCategoryName = request.SubCategoryName,
            CategoryId = request.CategoryId
        };

        await _unitOfWork.GetWriteRepository<SubCategory>().AddAsync(subCategory);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
