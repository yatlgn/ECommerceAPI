using ECommerceAPI.Application.Features.SubCategories.Command.CreateSubCategory;
using ECommerceAPI.Application.Features.SubCategories.Command.DeleteSubCategory;
using ECommerceAPI.Application.Features.SubCategories.Command.UpdateSubCategory;
using ECommerceAPI.Application.Features.SubCategories.Queries.GetAllSubCategories;
using ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoriesByCategoryId;
using ECommerceAPI.Application.Features.SubCategories.Queries.GetSubCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("ECommerceAPI/[controller]/[action]")]
[ApiController]
[Authorize]
public class SubCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var subCategories = await _mediator.Send(new GetAllSubCategoriesQueryRequest());
        return Ok(subCategories);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var subCategory = await _mediator.Send(new GetSubCategoryByIdQueryRequest { Id = id });
        if (subCategory == null) return NotFound();
        return Ok(subCategory);
    }

    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var subCategories = await _mediator.Send(new GetSubCategoriesByCategoryIdQueryRequest { CategoryId = categoryId });
        return Ok(subCategories);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSubCategoryCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, "SubCategory created successfully.");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSubCategoryCommandRequest request)
    {
        if (id != request.Id) return BadRequest("Id mismatch.");
        await _mediator.Send(request);
        return Ok("SubCategory updated successfully.");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteSubCategoryCommandRequest { Id = id });
        return Ok("SubCategory deleted successfully.");
    }
}

