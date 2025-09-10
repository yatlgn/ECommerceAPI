using ECommerceAPI.Application.Features.Addresses.Command.CreateAddress;
using ECommerceAPI.Application.Features.Addresses.Command.DeleteAddress;
using ECommerceAPI.Application.Features.Addresses.Commands.UpdateAddress;
using ECommerceAPI.Application.Features.Addresses.Queries.GetAllAddresses;
using ECommerceAPI.Application.Features.Addresses.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, "Address created successfully.");
        }

  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressCommandRequest request)
        {
            if (id != request.Id)
                return BadRequest("Id mismatch.");

            await _mediator.Send(request);
            return Ok("Address updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAddressCommandRequest { Id = id };
            await _mediator.Send(command);
            return Ok("Address deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetAddressByIdQueryRequest { Id = id };
            var address = await _mediator.Send(query);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAddressesQueryRequest();
            var addresses = await _mediator.Send(query);
            return Ok(addresses);
        }
    }
}
