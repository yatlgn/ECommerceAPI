using ECommerceAPI.Application.Features.Products.Command.CreateProduct;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,Unit >
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            var productRepo = _unitOfWork.GetWriteRepository<Product>();
            await productRepo.AddAsync(product);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
