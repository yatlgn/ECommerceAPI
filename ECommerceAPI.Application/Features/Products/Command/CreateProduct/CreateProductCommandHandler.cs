using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Products.Command.CreateProduct;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,ProductDto >
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId
            };

            var productRepo = _unitOfWork.GetWriteRepository<Product>();
            await productRepo.AddAsync(product);
            await _unitOfWork.SaveAsync();

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId
            };
            ;

            return productDto;
        }
    }
}
