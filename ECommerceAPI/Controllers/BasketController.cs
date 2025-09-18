using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Baskets.Command.AddToBasket;
using ECommerceAPI.Application.Features.Baskets.Command.RemoveFromBasket;
using ECommerceAPI.Application.Features.Baskets.Queries.GetBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{

    [Route("ECommerceAPI/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(Guid userId)
        {
            var query = new GetBasketQueryRequest { UserId = userId };
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody] AddToBasketCommandRequest request)
        {
            var basket = await _mediator.Send(request);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket([FromBody] RemoveFromBasketCommandRequest request)
        {
            var basket = await _mediator.Send(request);
            return Ok(basket);
        }
    }
}
