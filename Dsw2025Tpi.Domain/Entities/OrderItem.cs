using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class OrderItem:EntityBase
    {
        public Guid orderId { get; set; }
        
       // public Order _order { get; set; }

        public Guid productId { get; set; }
       // public Product _product { get; set; }

        public int quantity { get; set; }
        public decimal unitPrice { get; set; }

        public decimal subtotal=> quantity * unitPrice;

        public OrderItem(Guid OrderId,Guid ProductId, Product Product, int Quantity, decimal UnitPrice)
        {
            orderId = OrderId;
           // _order = order;
            productId = ProductId;
           // _product = product;
            quantity = Quantity;
            unitPrice = UnitPrice;
        }

        public OrderItem() { }
   


}
}
