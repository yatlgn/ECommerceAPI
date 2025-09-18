using ECommerceAPI.Domain.Common;
using System;
using System.Collections.Generic;

namespace ECommerceAPI.Domain.Entities
{
    public class Basket : IEntityBase
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
    }
}
