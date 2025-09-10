using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>()
                            .GetAsync(p => p.Id == request.Id);

            if (product == null)
                throw new Exception("Product not found.");

            await _unitOfWork.GetWriteRepository<Product>().HardDeleteAsync(product);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
