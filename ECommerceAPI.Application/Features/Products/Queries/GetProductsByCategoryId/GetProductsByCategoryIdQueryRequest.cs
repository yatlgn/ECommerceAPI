using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryRequest : IRequest<List<GetProductsByCategoryIdQueryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
