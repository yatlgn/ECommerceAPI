using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommandRequest>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty");
        }
    }
}
