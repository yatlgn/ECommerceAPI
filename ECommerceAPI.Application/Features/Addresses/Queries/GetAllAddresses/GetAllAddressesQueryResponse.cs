using System;

namespace ECommerceAPI.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQueryResponse
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Guid UserId { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
