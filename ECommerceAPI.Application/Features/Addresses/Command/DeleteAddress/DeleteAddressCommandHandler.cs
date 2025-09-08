using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Addresses.Command.DeleteAddress
{
    public class DeleteAddressCommandHandler : BaseHandler, IRequestHandler<DeleteAddressCommandRequest, Unit>
    {
        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(null!, unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
        {
            
            var readRepo = unitOfWork.GetReadRepository<Domain.Entities.Address>();
            var address = await readRepo.GetAsync(a => a.Id == request.Id);

            if (address == null)
                return Unit.Value; 

          
            var writeRepo = unitOfWork.GetWriteRepository<Domain.Entities.Address>();
            await writeRepo.HardDeleteAsync(address);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
