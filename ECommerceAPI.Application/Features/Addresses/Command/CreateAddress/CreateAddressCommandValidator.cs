using FluentValidation;

namespace ECommerceAPI.Application.Features.Addresses.Command.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommandRequest>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
        }
    }
}
