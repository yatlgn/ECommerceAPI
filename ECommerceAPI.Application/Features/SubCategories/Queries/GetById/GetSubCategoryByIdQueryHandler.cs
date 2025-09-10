using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQueryRequest, SubCategory>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SubCategory> Handle(GetSubCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var subCategory = await _unitOfWork.GetReadRepository<SubCategory>()
                .GetAsync(sc => sc.Id == request.Id);
            return subCategory;
        }
    }
}
