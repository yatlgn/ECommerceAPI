using FluentValidation;

namespace ECommerceAPI.Application.Features.Addresses.Command.DeleteAddress
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommandRequest>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address Id is required for deletion.");
        }
    }
}
