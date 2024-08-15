using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechWave.Models.DomainModel
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalTax { get; set; }

        [Required]
        [MaxLength(200)]
        public string ShippingAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        // Billing Information
        [Required]
        [MaxLength(200)]
        public string BillingAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string BillingCity { get; set; }

        [Required]
        [MaxLength(50)]
        public string BillingState { get; set; }

        [Required]
        [MaxLength(10)]
        public string BillingZipCode { get; set; }

        // Payment Information
        [Required]
        [MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string CardName { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [MaxLength(4)]
        public string CVV { get; set; }
    }
}
