using ECommerceAPI.Domain.Common;
using System;

namespace ECommerceAPI.Domain.Entities
{
    public class BasketProduct : IEntityBase
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
