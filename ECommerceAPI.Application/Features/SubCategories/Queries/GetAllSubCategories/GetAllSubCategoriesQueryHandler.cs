using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class GetAllSubCategoriesQueryHandler : IRequestHandler<GetAllSubCategoriesQueryRequest, IList<GetAllSubCategoriesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSubCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllSubCategoriesQueryResponse>> Handle(GetAllSubCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var subCategories = await _unitOfWork.GetReadRepository<Domain.Entities.SubCategory>()
                .GetAllAsync(include: sc => sc.Include(s => s.Category));

            return subCategories.Select(sc => new GetAllSubCategoriesQueryResponse
            {
                Id = sc.Id,
                SubCategoryName = sc.SubCategoryName,
                CategoryId = sc.CategoryId,
                CategoryName = sc.Category.CategoryName
            }).ToList();
        }
    }
}
