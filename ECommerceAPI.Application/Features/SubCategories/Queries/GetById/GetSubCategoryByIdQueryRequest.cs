using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryRequest : IRequest<SubCategory>
    {
        public int Id { get; set; }
    }
}
