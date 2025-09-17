using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Products.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQueryRequest, List<GetProductsByCategoryIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsByCategoryIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetProductsByCategoryIdQueryResponse>> Handle(GetProductsByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Domain.Entities.Product>()
                .GetAllAsync(
                    p => p.CategoryId == request.CategoryId,
                    include: p => p.Include(x => x.Category).Include(x => x.SubCategory)
                );

            return products.Select(p => new GetProductsByCategoryIdQueryResponse
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
