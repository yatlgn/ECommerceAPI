using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Queries.GetById
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQueryRequest, Address>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAddressByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.GetReadRepository<Address>()
                                           .GetAsync(a => a.Id == request.Id);

            if (address == null)
                throw new KeyNotFoundException("Address not found.");

            return address;
        }
    }
}
