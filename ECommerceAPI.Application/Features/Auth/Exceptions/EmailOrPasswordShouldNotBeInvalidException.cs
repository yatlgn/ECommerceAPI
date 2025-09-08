using ECommerceAPI.Application.Bases;

namespace ECommerceAPI.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException : BaseException
    {
        public EmailOrPasswordShouldNotBeInvalidException() : base("Email or password is incorrect!") { }

    }

    }
      

