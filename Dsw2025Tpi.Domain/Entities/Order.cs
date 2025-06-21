using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order : EntityBase
    {

        public Order(string shippingAddress, string billingAddress, DateTime createdAt)
        {
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Id = Guid.NewGuid();
            CreatedAt = createdAt;
            

        }
        public Customer Customer { get; set; } 
        public OrderStatus Status { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; } 
        
    }
}
