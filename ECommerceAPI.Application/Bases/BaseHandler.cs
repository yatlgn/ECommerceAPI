using AutoMapper;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace ECommerceAPI.Application.Bases
{
    public class BaseHandler
    {
        public readonly IMapper mapper;
        public readonly IUnitOfWork unitOfWork;
        public readonly IHttpContextAccessor httpContextAccessor;
        public Guid UserId { get; }

        public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;

            var httpContext = httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (Guid.TryParse(userIdClaim, out var userId))
            {
                UserId = userId;
            }
            else
            {
                UserId = Guid.Empty; 
            }
        }
    }
}
