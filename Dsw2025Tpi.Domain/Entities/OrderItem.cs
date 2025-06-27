using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public OrderItem() { }
        public OrderItem(Guid productId, int quantity, string productName, string productDesciption, decimal unitPrice, Guid orderId)
        {
            ProductId = productId;
            Quantity = quantity;
            Product.Name = productName;
            Product.Description = productDesciption;
            UnitPrice = unitPrice;
            SubTotal = Quantity * UnitPrice;
            OrderId = OrderId;
        }
        public Product? Product { get; set; }
        public Guid ProductId { get; }
        public Guid OrderId { get; }
    }
}
