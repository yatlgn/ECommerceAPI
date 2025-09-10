using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
