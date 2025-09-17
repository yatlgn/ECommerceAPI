using AutoMapper;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new ApplicationException("User not found");

            if (!string.IsNullOrEmpty(request.Name))
            {
                user.UserName = request.Name;
                user.NormalizedUserName = request.Name.ToUpper(); 
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
                user.NormalizedEmail = request.Email.ToUpper();
            }

            if (!string.IsNullOrEmpty(request.Surname))
                user.UserSurname = request.Surname;

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;

            if (!string.IsNullOrEmpty(request.Gender))
                user.Gender = request.Gender;

            if (request.BirthDate.HasValue)
                user.BirthDate = DateTime.SpecifyKind(request.BirthDate.Value, DateTimeKind.Utc);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                UserSurname = user.UserSurname,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                BirthDate = user.BirthDate
            };

            return userDto;
        }
    }
}