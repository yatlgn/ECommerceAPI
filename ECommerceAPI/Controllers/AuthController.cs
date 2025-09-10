using ECommerceAPI.Application.Features.Auth.Command.Login;
using ECommerceAPI.Application.Features.Auth.Command.RefreshToken;
using ECommerceAPI.Application.Features.Auth.Command.Register;
using ECommerceAPI.Application.Features.Auth.Command.Revoke;
using ECommerceAPI.Application.Features.Auth.Command.RevokeAll;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public AuthController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Token successfully revoked." });
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            await _mediator.Send(new RevokeAllCommandRequest());
            return Ok(new { Message = "All tokens have been successfully canceled." });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] string idToken)
        {
            try
            {
              
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                var email = payload.Email;

               
                var jwt = GenerateJwtToken(email);

                return Ok(new
                {
                    token = jwt,
                    email,
                    userName = payload.Name
                });
            }
            catch
            {
                return BadRequest("Geçersiz Google token");
            }
        }

        private string GenerateJwtToken(string email)
        {
            var tokenSettings = _config.GetSection("TokenSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.Email, email) },
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(tokenSettings["TokenValidityInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
