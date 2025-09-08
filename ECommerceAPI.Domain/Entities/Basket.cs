using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Basket
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } 

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}