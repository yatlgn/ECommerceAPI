using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.Login
{
    public class LoginCommandResponse
    {
        public string Name { get; set; } 
        public string Surname { get; set; } 
        public string Email { get; set; } 
        public string Token { get; set; } 
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; } = DateTime.UtcNow;
    }
}
