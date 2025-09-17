using MediatR;
using System.Collections.Generic;

namespace ECommerceAPI.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQueryRequest : IRequest<IList<GetAllAddressesQueryResponse>>
    {
      
         public Guid UserId { get; set; }
    }
}
