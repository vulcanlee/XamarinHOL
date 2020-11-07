using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
