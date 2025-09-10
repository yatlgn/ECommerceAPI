using ECommerceAPI.Application.Features.Categories.Command.CreateCategory;
using ECommerceAPI.Application.Features.Categories.Command.DeleteCategory;
using ECommerceAPI.Application.Features.Categories.Command.UpdateCategory;
using ECommerceAPI.Application.Features.Categories.Queries.GetAllCategories;
using ECommerceAPI.Application.Features.Categories.Queries.GetAllCategoriesWithSubCategories;
using ECommerceAPI.Application.Features.Categories.Queries.GetById;
using ECommerceAPI.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{
    [Route("ECommerceAPI/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQueryRequest());
            return Ok(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllWithSubCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesWithSubCategoriesQueryRequest());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQueryRequest { Id = id });
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode(201, "Category created successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommandRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch.");
            await _mediator.Send(request);
            return Ok("Category updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommandRequest { Id = id });
            return Ok("Category deleted successfully.");
        }
    }

}
