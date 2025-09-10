using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Command.CreateSubCategory
{
    public class CreateSubCategoryCommandRequest : IRequest<Unit>
    {
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
    }

}
