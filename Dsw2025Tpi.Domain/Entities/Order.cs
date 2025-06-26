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
        public Order(string? shippindAddress, string? billingAddress, string? notes, List<OrderItem> orderItems,Guid customerId)
        {
            DateTime = DateTime.UtcNow;
            ShippindAddress = shippindAddress;
            BillingAddress = billingAddress;
            Notes = notes;
            TotalAmount = OrderItems.Sum(item => item.SubTotal);
            OrderItems = orderItems;
            CustomerId = customerId;
        }
        public required Customer Customer { get; set; }
        public Guid CustomerId { get; }
    }
}
