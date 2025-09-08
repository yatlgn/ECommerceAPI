using AutoMapper;
using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.Features.Auth.Command.RefreshToken;
using ECommerceAPI.Application.Features.Auth.Rules;
using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.RefreshToken
{

    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly ITokenService tokenService;
        public RefreshTokenCommandHandler(IMapper mapper, AuthRules aut, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            string email = principal.FindFirstValue(ClaimTypes.Email);

            Domain.Entities.User? user = await userManager.FindByEmailAsync(email);
            IList<string> roles = await userManager.GetRolesAsync(user);


            await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiryTime);

            JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
            string newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
