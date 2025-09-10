using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>()
                            .GetAsync(p => p.Id == request.Id);

            if (product == null)
                throw new Exception("Product not found.");

            product.ProductName = request.ProductName;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
