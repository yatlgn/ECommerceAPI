using ECommerceAPI.Domain.Common;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }
        public string Street { get; set; } 
        public string City { get; set; }
        public string Country { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } 
    }
}
