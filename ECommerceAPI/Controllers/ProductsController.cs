using ECommerceAPI.Application.Features.Products.Command.CreateProduct;
using ECommerceAPI.Application.Features.Products.Command.DeleteProduct;
using ECommerceAPI.Application.Features.Products.Command.UpdateProduct;
using ECommerceAPI.Application.Features.Products.Queries.GetAllProducts;
using ECommerceAPI.Application.Features.Products.Queries.GetProductById;
using ECommerceAPI.Application.Features.Products.Queries.GetProductsByCategoryId;
using ECommerceAPI.Application.Features.Products.Queries.GetProductsBySubCategoryId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{
    [Route("ECommerceAPI/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("GetProductsByCategory/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var product = await _mediator.Send(new GetProductsByCategoryIdQueryRequest { CategoryId = categoryId });
            return Ok(product);
        }

        [HttpGet("GetProductsBySubCategory/{subcategoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsBySubCategory(int subcategoryId)
        {
            var product = await _mediator.Send(new GetProductsBySubCategoryIdQueryRequest { SubCategoryId = subcategoryId });
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, "Product created successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommandRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch.");
            await _mediator.Send(request);
            return Ok("Product updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommandRequest { Id = id });
            return Ok("Product deleted successfully.");
        }
    }

}
