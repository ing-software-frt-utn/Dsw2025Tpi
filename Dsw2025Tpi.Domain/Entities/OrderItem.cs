using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    internal class OrderItem : EntityBase
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Product Product { get; set; }
        public OrderItem(int quantity, decimal unitPrice, Product product)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Product = product;
        }
        public decimal SubTotal() => Quantity * UnitPrice;
    }
}
