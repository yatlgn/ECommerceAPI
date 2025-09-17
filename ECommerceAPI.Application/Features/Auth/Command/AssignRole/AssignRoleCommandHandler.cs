using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommandRequest, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AssignRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(request.RoleName));
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

            return $"Role '{request.RoleName}' assigned to {user.UserName}";
        }
    }
}
