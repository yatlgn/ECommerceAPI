using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities; // Mevcut Address entity’si buradan
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Command.CreateAddress
{
    public class CreateAddressCommandHandler : BaseHandler, IRequestHandler<CreateAddressCommandRequest, Unit>
    {
        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(null!, unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = new Address
            {
                UserId = UserId,
                Street = request.Street,
                City = request.City,
                Country = request.Country
            };

            var writeRepo = unitOfWork.GetWriteRepository<Address>();
            await writeRepo.AddAsync(address);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
