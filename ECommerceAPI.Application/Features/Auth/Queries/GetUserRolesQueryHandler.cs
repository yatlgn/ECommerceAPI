using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Queries
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
    {
        private readonly UserManager<User> _userManager;

        public GetUserRolesQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
    }
}
