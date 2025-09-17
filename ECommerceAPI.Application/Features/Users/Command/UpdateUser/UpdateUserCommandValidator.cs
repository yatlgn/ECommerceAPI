using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty");

            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");

            RuleFor(x => x.Surname)
                .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email address");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage("Phone number cannot be empty")
               .Matches(@"^\+?\d{10,15}$").WithMessage("Enter a valid phone number with country code if needed");

            RuleFor(x => x.Gender)
                .Must(g => g == "Male" || g == "Female" || g == "Other" || string.IsNullOrEmpty(g))
                .WithMessage("Gender must be Male, Female or Other");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).When(x => x.BirthDate.HasValue)
                .WithMessage("BirthDate cannot be in the future");
        }
    }
}
