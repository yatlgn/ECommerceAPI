using ECommerceAPI.Application.Features.Auth.Command.Register;
using FluentValidation;

namespace ECommerceAPI.Application.Features.Auth.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .WithName("User Name");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MinimumLength(2)
                .WithName("Surname");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-mail Address");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[0-9]{7,15}$") // Basit telefon numarası doğrulaması
                .WithMessage("Phone number is invalid")
                .WithName("Phone Number");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Today)
                .WithMessage("Birth date must be in the past")
                .WithName("Birth Date");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be Male, Female, or Other")
                .WithName("Gender");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match")
                .WithName("Password Repeat");
        }
    }
}
