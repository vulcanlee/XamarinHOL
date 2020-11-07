using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string Name { get; set; }
        public byte Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
