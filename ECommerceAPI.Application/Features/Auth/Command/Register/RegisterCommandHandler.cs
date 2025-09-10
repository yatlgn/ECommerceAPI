using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.Features.Auth.Rules;
using ECommerceAPI.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;

namespace ECommerceAPI.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public RegisterCommandHandler(
            AuthRules authRules,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Email üzerinden var mı kontrol
                var existingUser = await userManager.FindByEmailAsync(request.Email);
                await authRules.UserShouldNotBeExist(existingUser);

                if (request.Password != request.ConfirmPassword)
                    throw new Exception("Passwords do not match.");

                // UserName üretimi
                string generatedUserName;
                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    generatedUserName = request.UserName;
                }
                else if (!string.IsNullOrWhiteSpace(request.Email) && request.Email.Contains("@"))
                {
                    generatedUserName = request.Email.Split('@')[0];
                }
                else
                {
                    generatedUserName = Guid.NewGuid().ToString("N");
                }

                // User objesi oluşturuluyor, tüm DateTime alanları UTC
                User user = new User
                {
                    UserName = generatedUserName,
                    Surname = request.Surname,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate.HasValue
                        ? DateTime.SpecifyKind(request.BirthDate.Value, DateTimeKind.Utc)
                        : null,
                    Gender = request.Gender,
                    NormalizedEmail = request.Email?.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7) // Örnek token süresi
                };

                // User creation
                IdentityResult result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"User creation failed: {errors}");
                }

                // Rol kontrol ve ekleme
                if (!await roleManager.RoleExistsAsync("user"))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>
                    {
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });
                }

                await userManager.AddToRoleAsync(user, "user");

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
        }
    }
}
