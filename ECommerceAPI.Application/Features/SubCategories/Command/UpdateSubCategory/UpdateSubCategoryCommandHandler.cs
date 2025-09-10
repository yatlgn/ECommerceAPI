using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Command.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSubCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<SubCategory>();
            var subCategory = await repo.GetAsync(s => s.Id == request.Id);
            if (subCategory == null) throw new Exception("SubCategory not found");

            subCategory.SubCategoryName = request.SubCategoryName;
            subCategory.CategoryId = request.CategoryId;

            await _unitOfWork.GetWriteRepository<SubCategory>().UpdateAsync(subCategory);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
