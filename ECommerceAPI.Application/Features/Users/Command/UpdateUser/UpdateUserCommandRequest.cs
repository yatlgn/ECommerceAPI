using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserCommandRequest : IRequest<UserDto>
    {
        public Guid? UserId { get; set; }   
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
