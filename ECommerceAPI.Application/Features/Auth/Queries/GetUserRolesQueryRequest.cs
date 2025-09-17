using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Queries
{
    public class GetUserRolesQuery : IRequest<IList<string>>
    {
        public Guid UserId { get; set; }
    }
}
