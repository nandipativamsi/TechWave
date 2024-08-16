using System.Collections.Generic;
using TechWave.Models.DomainModel;

public class CartSummary
{
    public IEnumerable<Cart> Items { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}