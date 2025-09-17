using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{

    public class Auth
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } 
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
