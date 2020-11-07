using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
