using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Queries.GetProductsBySubCategoryId
{
    public class GetProductsBySubCategoryIdQueryRequest : IRequest<List<GetProductsBySubCategoryIdQueryResponse>>
    {
        public int SubCategoryId { get; set; }
    }
}
