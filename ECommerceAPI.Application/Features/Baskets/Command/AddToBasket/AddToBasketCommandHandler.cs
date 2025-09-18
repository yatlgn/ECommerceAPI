using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Baskets.Command.AddToBasket;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Baskets.Command.AddToBasket
{
    public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommandRequest, BasketDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToBasketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDto> Handle(AddToBasketCommandRequest request, CancellationToken cancellationToken)
        {
   
            var basket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAllAsync(
                    b => b.UserId == request.UserId,
                    include: q => q.Include(b => b.BasketProducts)
                                   .ThenInclude(bp => bp.Product)
                );

            var userBasket = basket.FirstOrDefault();
            if (userBasket == null)
            {
                userBasket = new Basket { UserId = request.UserId };
                await _unitOfWork.GetWriteRepository<Basket>().AddAsync(userBasket);
                await _unitOfWork.SaveAsync();
            }

            var existingProduct = userBasket.BasketProducts.FirstOrDefault(bp => bp.ProductId == request.ProductId);
            if (existingProduct != null)
                existingProduct.Quantity++;
            else
            {
                var product = await _unitOfWork.GetReadRepository<Product>()
                    .GetAsync(p => p.Id == request.ProductId);
                if (product == null) throw new Exception("Product not found");

                userBasket.BasketProducts.Add(new BasketProduct
                {
                    Basket = userBasket,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = 1
                });
            }

            await _unitOfWork.GetWriteRepository<Basket>().UpdateAsync(userBasket);
            await _unitOfWork.SaveAsync();

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
