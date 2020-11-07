using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.AdapterModels
{
    public class OrderItemAdapterModel
    {
        public int OrderItemId { get; set; }
        [Required(ErrorMessage = "訂單項目名稱 欄位必須要輸入值")]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public OrderAdapterModel Order { get; set; }
        public ProductAdapterModel Product { get; set; }
    }
}
