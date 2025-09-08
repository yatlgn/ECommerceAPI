using MediatR;

namespace ECommerceAPI.Application.Features.Addresses.Command.DeleteAddress
{
    public class DeleteAddressCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; } 
    }
}
