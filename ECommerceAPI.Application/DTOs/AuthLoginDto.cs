using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
public class AuthLoginDto
{
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }

public class AuthResponseDto
{
    public UserDto User { get; set; }
    public TokenDto Token { get; set; }
}
}
