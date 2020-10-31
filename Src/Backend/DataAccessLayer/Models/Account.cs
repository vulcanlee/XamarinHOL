using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public bool Disable { get; set; }
    }
}
