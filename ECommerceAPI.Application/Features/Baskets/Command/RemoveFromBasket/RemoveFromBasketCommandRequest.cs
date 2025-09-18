using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Baskets.Command.RemoveFromBasket
{
    public class RemoveFromBasketCommandRequest : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
    }
}
