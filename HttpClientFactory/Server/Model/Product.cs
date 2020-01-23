using System;

namespace Server.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
    }
}