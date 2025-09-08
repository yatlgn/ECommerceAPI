using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class OrderCreateDto
    {
        public Guid UserId { get; set; }
        public List<int> ProductIds { get; set; }  
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public UserDto User { get; set; }
        public List<ProductDto> Products { get; set; }
    }

}
