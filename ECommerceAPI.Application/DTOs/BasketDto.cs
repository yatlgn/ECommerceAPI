using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public List<BasketProductDto> Products { get; set; } = new();
    }
}
