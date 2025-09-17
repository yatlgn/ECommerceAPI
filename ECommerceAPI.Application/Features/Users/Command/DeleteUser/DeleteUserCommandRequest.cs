using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Users.Command.DeleteUser
{
    public class DeleteUserCommandRequest : IRequest<Unit>
    {
        public Guid UserId { get; set; }  
    }
}
