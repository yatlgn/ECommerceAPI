using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.SubCategories.Command.DeleteSubCategory
{
    public class DeleteSubCategoryCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
