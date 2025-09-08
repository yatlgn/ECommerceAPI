using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenSettings _tokenSettings;

        public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
        {
            _tokenSettings = options.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (roles == null || !roles.Any()) roles = new List<string> { "user" };

            if (string.IsNullOrEmpty(_tokenSettings.Secret))
                throw new ArgumentNullException(nameof(_tokenSettings.Secret));

            if (_tokenSettings.TokenValidityInMinutes <= 0)
                throw new ArgumentOutOfRangeException(nameof(_tokenSettings.TokenValidityInMinutes));

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email)
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.TokenValidityInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
            );

            await _userManager.AddClaimsAsync(user, claims);

            return token;
        }


        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
