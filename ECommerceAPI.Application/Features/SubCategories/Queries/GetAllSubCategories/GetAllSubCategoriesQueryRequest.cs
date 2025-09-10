using MediatR;
using System.Collections.Generic;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class GetAllSubCategoriesQueryRequest : IRequest<IList<GetAllSubCategoriesQueryResponse>>
    {
    }
}
