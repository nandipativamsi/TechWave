namespace TechWave.Models.DomainModel
{
    public class Order
    {
        public int Id { get; set; } // Primary key

        // Shipping Information
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment Information
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }

        // Order Summary
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
    }
}