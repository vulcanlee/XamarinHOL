using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
            Staff = new HashSet<Staff>();
            Stock = new HashSet<Stock>();
        }

        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
