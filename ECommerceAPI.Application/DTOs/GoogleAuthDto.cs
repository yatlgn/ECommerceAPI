using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class GoogleAuthDto
    {
        public string IdToken { get; set; }
        public string Provider { get; set; } 
        public string ProviderKey { get; set; }
        public Guid UserId { get; set; }
    }
}
