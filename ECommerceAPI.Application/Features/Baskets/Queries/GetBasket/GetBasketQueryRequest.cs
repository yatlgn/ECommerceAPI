using ECommerceAPI.Application.DTOs;
using MediatR;
using System;

namespace ECommerceAPI.Application.Features.Baskets.Queries.GetBasket
{
    public class GetBasketQueryRequest : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
    }
}
