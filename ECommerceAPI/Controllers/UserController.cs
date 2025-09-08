using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                              ?? User?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();

            if (!Guid.TryParse(userIdClaim, out Guid userId)) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound("User not found.");

            return Ok(new
            {
                user.UserName,
                user.Email,
                user.Surname,
                user.PhoneNumber,
                BirthDate = user.BirthDate?.ToString("yyyy-MM-dd"),
                user.Gender
            });
        }
    }
}
