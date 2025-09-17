using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Features.Users.Command.DeleteUser;
using ECommerceAPI.Application.Features.Users.Command.UpdateUser;
using ECommerceAPI.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommandRequest request)
        {
            if(request.UserId == Guid.Empty)
                return BadRequest("UserId is required for anonymous update.");
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommandRequest { UserId = id });
            return Ok("User deleted successfully");
        }
     

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
    
            var result = await _mediator.Send(new GetUserByIdRequest { Id = id });
            if (result == null) return NotFound("User not found.");
            return Ok(result);
        }
    }
}
