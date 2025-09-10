using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ECommerceAPI.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {

        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; } 
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public ICollection<GoogleAuth> GoogleAuths { get; set; } = new List<GoogleAuth>();
    }
}
