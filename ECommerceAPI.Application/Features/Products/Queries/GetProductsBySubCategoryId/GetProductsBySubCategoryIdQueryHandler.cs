using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Products.Queries.GetProductsBySubCategoryId
{
    public class GetProductsBySubCategoryIdQueryHandler : IRequestHandler<GetProductsBySubCategoryIdQueryRequest, List<GetProductsBySubCategoryIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsBySubCategoryIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetProductsBySubCategoryIdQueryResponse>> Handle(GetProductsBySubCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Domain.Entities.Product>()
                .GetAllAsync(
                    p => p.SubCategoryId == request.SubCategoryId,
                    include: p => p.Include(x => x.Category).Include(x => x.SubCategory)
                );

            return products.Select(p => new GetProductsBySubCategoryIdQueryResponse
            {
                Id = p.Id,
                Name = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.CategoryName,
                SubCategoryName = p.SubCategory.SubCategoryName
            }).ToList();
        }
    }
}
