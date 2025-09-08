using MediatR;

namespace ECommerceAPI.Application.Features.Addresses.Command.CreateAddress
{
    
    public class CreateAddressCommandRequest : IRequest<Unit>
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
