using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoriesByCategoryId
{
    public class GetSubCategoriesByCategoryIdQueryRequest : IRequest<IList<GetSubCategoriesByCategoryIdQueryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
