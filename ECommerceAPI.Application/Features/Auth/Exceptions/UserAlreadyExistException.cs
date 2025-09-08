
using ECommerceAPI.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base("Such a user already exists!") { }
    }
}
