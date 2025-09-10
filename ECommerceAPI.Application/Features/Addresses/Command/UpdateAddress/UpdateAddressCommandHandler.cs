using ECommerceAPI.Application.Interfaces.UnitOfWorks; // IUnitOfWork burada
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.GetReadRepository<Address>()
                                           .GetAsync(a => a.Id == request.Id);

            if (address == null)
                throw new Exception("Address not found.");

            address.City = request.City;
            address.Street = request.Street;

            await _unitOfWork.GetWriteRepository<Address>().UpdateAsync(address);
            await _unitOfWork.SaveAsync(); 

            return Unit.Value;
        }
    }
}
