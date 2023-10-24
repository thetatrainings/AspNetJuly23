﻿namespace ThetaEcommerce.DTOs
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public string? Sku { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
