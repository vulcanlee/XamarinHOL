using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{
    public class LoginRequestDTO
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
