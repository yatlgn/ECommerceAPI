using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserDto>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
     
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
                throw new ApplicationException("User not found");


            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                UserSurname = user.UserSurname,
                BirthDate = user.BirthDate,
                Gender = user.Gender


            };

            return userDto;
        }
    }
}
