﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObject.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
