using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Queries.GetById
{
    public class GetAddressByIdQueryRequest : IRequest<Address>
    {
        public int Id { get; set; }
    }
}
