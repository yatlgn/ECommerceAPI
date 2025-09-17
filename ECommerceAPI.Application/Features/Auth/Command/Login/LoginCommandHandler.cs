using AutoMapper;
using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.Features.Auth.Rules;
using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly AuthRules authRules;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;
        public LoginCommandHandler(UserManager<Domain.Entities.User> userManager, IConfiguration configuration, ITokenService tokenService, RoleManager<IdentityRole<Guid>> roleManager, AuthRules authRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.userManager = userManager;
            this.authRules = authRules;
            this.roleManager = roleManager;
            this.tokenService = tokenService;
            this.configuration = configuration;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {

            User? user = await userManager.FindByEmailAsync(request.Email);


            bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);


            await authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);


            IList<string> roles = await userManager.GetRolesAsync(user);

            if (roles == null || !roles.Any())
            {
                roles = new List<string> { "user" };
            }

            JwtSecurityToken token = await tokenService.CreateToken(user, roles);
            string refreshToken = tokenService.GenerateRefreshToken();


            _ = int.TryParse(configuration["JWT : RefreshTokenValidityInDays"], out int refreshTokenValidiyInDays);


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidiyInDays);

            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);


            string _token = new JwtSecurityTokenHandler().WriteToken(token);
            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);


            return new LoginCommandResponse
            {

                Email = user.Email,
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}
