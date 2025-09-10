using ECommerceAPI.Application.Features.Auth.Command.AssignRole;
using ECommerceAPI.Application.Features.Auth.Queries;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



[Route("ECommerceAPI/[controller]/[action]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{userId}/assign-role")]
    public async Task<IActionResult> AssignRole(string userId, [FromQuery] string roleName)
    {
        var result = await _mediator.Send(new AssignRoleCommandRequest { UserId = userId, RoleName = roleName });
        return Ok(result);
    }

    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var roles = await _mediator.Send(new GetUserRolesQuery { UserId = userId });
        return Ok(roles);
    }
}
