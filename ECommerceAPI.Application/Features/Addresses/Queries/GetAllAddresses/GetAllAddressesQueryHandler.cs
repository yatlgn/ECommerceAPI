using AutoMapper;
using ECommerceAPI.Application.Features.Addresses.Queries.GetAllAddresses;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQueryRequest, IList<GetAllAddressesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllAddressesQueryResponse>> Handle(GetAllAddressesQueryRequest request, CancellationToken cancellationToken)
        {
            var addresses = await _unitOfWork.GetReadRepository<Address>().GetAllAsync();

         
            var response = _mapper.Map<IList<GetAllAddressesQueryResponse>>(addresses);

            return response;
        }
    }
}
