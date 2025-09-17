using ECommerceAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Product : IEntityBase
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}

