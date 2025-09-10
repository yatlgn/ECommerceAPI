using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Command.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSubCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<SubCategory>();
            var subCategory = await repo.GetAsync(s => s.Id == request.Id);
            if (subCategory == null) throw new Exception("SubCategory not found");

            await _unitOfWork.GetWriteRepository<SubCategory>().HardDeleteAsync(subCategory);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
