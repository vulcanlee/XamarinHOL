﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.AdapterModels
{
    public class HoluserAdapterModel
    {
        public int HoluserId { get; set; }
        [Required(ErrorMessage = "名稱 欄位必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "帳號 欄位必須要輸入值")]
        public string Account { get; set; }
        [Required(ErrorMessage = "密碼 欄位必須要輸入值")]
        public string Password { get; set; }
        public int TokenVersion { get; set; }
        public int Level { get; set; }

        public bool IsExist()
        {
            if (string.IsNullOrEmpty(Account))
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
