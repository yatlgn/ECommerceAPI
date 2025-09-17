using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null;
        public string? UserSurname { get; set; }
        public string Email { get; set; } = null;
        public string PhoneNumber { get; set; } = null;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<AddressDto> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Payment> Payments { get; set; }
        
    }
}
