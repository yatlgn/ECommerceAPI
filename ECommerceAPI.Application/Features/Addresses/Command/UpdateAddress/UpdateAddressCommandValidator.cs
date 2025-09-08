using FluentValidation;

namespace ECommerceAPI.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommandRequest>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Address Id must be greater than 0.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");
        }
    }
}
