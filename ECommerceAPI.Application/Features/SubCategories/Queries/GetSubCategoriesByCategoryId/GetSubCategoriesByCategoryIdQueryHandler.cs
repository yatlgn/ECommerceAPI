using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoriesByCategoryId
{
    public class GetSubCategoriesByCategoryIdQueryHandler : IRequestHandler<GetSubCategoriesByCategoryIdQueryRequest, IList<GetSubCategoriesByCategoryIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubCategoriesByCategoryIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetSubCategoriesByCategoryIdQueryResponse>> Handle(GetSubCategoriesByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            var subcategories = await _unitOfWork.GetReadRepository<ECommerceAPI.Domain.Entities.SubCategory>()
                .GetAllAsync(sc => sc.CategoryId == request.CategoryId);

            return subcategories.Select(sc => new GetSubCategoriesByCategoryIdQueryResponse
            {
                Id = sc.Id,
                SubCategoryName = sc.SubCategoryName,
                CategoryId = sc.CategoryId
            }).ToList();
        }
    }
}
