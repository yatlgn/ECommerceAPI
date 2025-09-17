using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, Unit>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new ApplicationException("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return Unit.Value;  
        }
    }
}
