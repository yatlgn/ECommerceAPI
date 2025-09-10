using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQueryRequest : IRequest<Category>
    {
        public int Id { get; set; }
    }
}
