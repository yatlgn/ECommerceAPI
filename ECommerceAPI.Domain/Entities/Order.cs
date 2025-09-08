using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } 

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
