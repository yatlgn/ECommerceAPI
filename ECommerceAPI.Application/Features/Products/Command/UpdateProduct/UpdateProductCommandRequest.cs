using MediatR;

namespace ECommerceAPI.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
