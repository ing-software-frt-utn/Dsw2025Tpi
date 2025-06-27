namespace Dsw2025Tpi.Domain.Entities
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        public OrderItem()
        { }
    }
}