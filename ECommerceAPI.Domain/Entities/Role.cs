using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; } = new List<User>();
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;

    }
}
