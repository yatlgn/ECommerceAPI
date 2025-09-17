using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.AssignRole
{
    public class AssignRoleCommandRequest : IRequest<string>
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
    }
}
