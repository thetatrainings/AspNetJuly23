using System;
using System.Collections.Generic;

namespace ThetaEcommerce.Models
{
    public partial class SystemUser
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
