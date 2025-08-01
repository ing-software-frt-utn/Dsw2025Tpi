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

        public Guid OrderId { get; set; } //Fk de Order
        public Order? Order { get; set; } //OrderItem sabe en que Order se encuentra 

        public Guid ProductId { get; set; } //Fk de Product
        public Product? Product { get; set; } //para saber a que producto pertenece OrderId

        public OrderItem()
        {
            
        }
        public OrderItem(int quantity, decimal unitPrice, int subTotal)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            SubTotal = subTotal;
        }
    }
}
