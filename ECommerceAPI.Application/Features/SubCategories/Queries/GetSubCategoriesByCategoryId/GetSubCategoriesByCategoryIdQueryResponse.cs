using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoriesByCategoryId
{
    public class GetSubCategoriesByCategoryIdQueryResponse
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
