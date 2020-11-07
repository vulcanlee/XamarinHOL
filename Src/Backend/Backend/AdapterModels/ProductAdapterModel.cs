using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.AdapterModels
{
    public class ProductAdapterModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "商品名稱 欄位必須要輸入值")]
        public string Name { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

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
