using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.AdapterModels
{
    public class OrderAdapterModel
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }


        public bool IsExist()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
