using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } 
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
