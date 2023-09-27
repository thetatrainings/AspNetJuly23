using System;
using System.Collections.Generic;

namespace ThetaEcommerce.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Cnic { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public int? SystemUserId { get; set; }
    }
}
