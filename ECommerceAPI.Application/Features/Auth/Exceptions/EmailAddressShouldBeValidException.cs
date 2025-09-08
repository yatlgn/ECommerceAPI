
using ECommerceAPI.Application.Bases;

namespace ECommerceAPI.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException : BaseException
    {
        public EmailAddressShouldBeValidException() : base("There is no such e-mail address.") { }
    }
}
      

