using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class PaymentCreateDto
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }

}
