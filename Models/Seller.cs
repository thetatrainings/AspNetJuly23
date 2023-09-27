using System;
using System.Collections.Generic;

namespace ThetaEcommerce.Models
{
    public partial class Seller
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? SellerId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? CompanyName { get; set; }
        public int? SystemUserId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
    }
}
