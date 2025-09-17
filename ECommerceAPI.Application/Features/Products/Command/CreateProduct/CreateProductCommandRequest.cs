using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<ProductDto>
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
