using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.Register
{
    public class RegisterCommandRequest : IRequest<Unit>
    {
        public string UserName { get; set; }    
        public string Surname { get; set; }       
        public string Email { get; set; }
        public string PhoneNumber { get; set; }   
        public DateTime? BirthDate { get; set; } 
        public string Gender { get; set; }        

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
