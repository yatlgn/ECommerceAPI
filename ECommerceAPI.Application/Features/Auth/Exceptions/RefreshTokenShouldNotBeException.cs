using ECommerceAPI.Application.Bases;

namespace ECommerceAPI.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotBeException : BaseException
    { 
        public  RefreshTokenShouldNotBeException() : base("The login period has expired. Please log in again.") { }
    }
}
      

