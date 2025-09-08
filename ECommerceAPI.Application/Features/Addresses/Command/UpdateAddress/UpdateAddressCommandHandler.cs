using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, Unit>
    {
        private readonly AppDbContext _context;

        public UpdateAddressCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (address == null)
                throw new Exception("Address not found.");

            address.City = request.City;
            address.Street = request.Street;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
