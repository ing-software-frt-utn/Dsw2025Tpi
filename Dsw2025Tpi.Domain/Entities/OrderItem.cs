﻿using System;
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
        public OrderItem(int quantity, Guid productId)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = Product!.CurrentUnitPrice;
            SubTotal = Quantity * UnitPrice;
        }
        public required Product Product { get; set; }
        public Guid ProductId { get; }
    }
}
