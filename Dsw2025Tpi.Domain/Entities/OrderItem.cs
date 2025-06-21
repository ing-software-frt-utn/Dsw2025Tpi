﻿using Dsw2025Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    internal class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public Order Order { get; set; } 
        public Product Product { get; set; }
        public required string Name { get; set; } 
        public required string Description { get; set; }
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }
        public decimal Subtotal => Quantity * UnitPrice; 
    }
}
