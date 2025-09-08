using ECommerceAPI.Application.DTOs;
using MediatR;

namespace ECommerceAPI.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; } 
        public string City { get; set; }
        public string Street { get; set; }
    }
}
