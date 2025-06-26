using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order : EntityBase
    {
        public DateTime DateTime { get; set; }
        public string? ShippindAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        public Order() { }
        public Order(Guid customerId, string? shippindAddress, string? billingAddress, List<OrderItem> orderItems, string? notes)
        {
            CustomerId = customerId;
            ShippindAddress = shippindAddress;
            BillingAddress = billingAddress;
            OrderItems = orderItems;
            DateTime = DateTime.UtcNow;
            Notes = notes;
            TotalAmount = OrderItems.Sum(item => item.SubTotal);
            
        }
        public required Customer Customer { get; set; }
        public Guid CustomerId { get; }
    }
}
