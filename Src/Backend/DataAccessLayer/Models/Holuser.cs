using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Holuser
    {
        public int HoluserId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int TokenVersion { get; set; }
        public int Level { get; set; }
    }
}
