using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Categories.Queries.GetAllCategoriesWithSubCategories
{

    public class GetAllCategoriesWithSubCategoriesQueryHandler : IRequestHandler<GetAllCategoriesWithSubCategoriesQueryRequest, List<GetAllCategoriesWithSubCategoriesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesWithSubCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllCategoriesWithSubCategoriesQueryResponse>> Handle(GetAllCategoriesWithSubCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.GetReadRepository<ECommerceAPI.Domain.Entities.Category>()
                .GetAllAsync(include: c => c.Include(x => x.SubCategories));

            return categories.Select(c => new GetAllCategoriesWithSubCategoriesQueryResponse
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                SubCategories = c.SubCategories.Select(sc => new SubCategoryDto
                {
                    Id = sc.Id,
                    SubCategoryName = sc.SubCategoryName
                }).ToList()
            }).ToList();
        }
    }
}
