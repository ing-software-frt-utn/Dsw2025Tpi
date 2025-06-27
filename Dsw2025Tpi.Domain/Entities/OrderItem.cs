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
        public OrderItem(int quantity, decimal unitPrice, Guid productId)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            SubTotal = Quantity * UnitPrice;
            ProductId = productId;
        }
        public Guid ProductId { get; }
        public Guid OrderId { get; }
    }
}
