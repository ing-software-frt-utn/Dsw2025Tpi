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
        public OrderItem(int quantity, Product product, Guid orderId)
        {
            product.ReduceStock(quantity);
            Quantity = quantity;
            Product = product;
            ProductId = product.Id;
            UnitPrice = product.CurrentUnitPrice;
            SubTotal = Quantity * UnitPrice;
            OrderId = OrderId;
        }
        public Product? Product { get; set; }
        public Guid ProductId { get; }
        public Guid OrderId { get; }
    }
}
