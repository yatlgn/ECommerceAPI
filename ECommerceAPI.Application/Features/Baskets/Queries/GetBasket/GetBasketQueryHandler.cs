using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Baskets.Queries.GetBasket;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Baskets.Queries.GetBasket
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQueryRequest, BasketDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBasketQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDto> Handle(GetBasketQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAllAsync(
                    b => b.UserId == request.UserId,
                    include: q => q.Include(b => b.BasketProducts)
                                   .ThenInclude(bp => bp.Product)
                );

            var userBasket = basket.FirstOrDefault();
            if (userBasket == null)
                return new BasketDto { UserId = request.UserId, Products = new List<BasketProductDto>() };

            return new BasketDto
            {
                UserId = userBasket.UserId,
                Products = userBasket.BasketProducts.Select(bp => new BasketProductDto
                {
                    ProductId = bp.ProductId,
                    Name = bp.Product.ProductName,
                    Price = bp.Product.Price,
                    Quantity = bp.Quantity
                }).ToList()
            };
        }
    }
}
