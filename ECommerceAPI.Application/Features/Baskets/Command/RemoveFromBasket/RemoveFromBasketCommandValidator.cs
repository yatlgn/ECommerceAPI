using ECommerceAPI.Application.Features.Baskets.Command.RemoveFromBasket;
using FluentValidation;

namespace ECommerceAPI.Application.Features.Baskets.Command.RemoveFromBasket
{
    public class RemoveFromBasketCommandValidator : AbstractValidator<RemoveFromBasketCommandRequest>
    {
        public RemoveFromBasketCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0");
        }
    }
}
