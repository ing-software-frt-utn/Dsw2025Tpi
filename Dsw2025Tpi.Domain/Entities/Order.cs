using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    internal class Order
    {
        public DateTime DateTime { get; set; }
        public string? ShippindAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? Notes { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        public Order(DateTime dateTime, string? shippindAddress, string? billingAddress, string? notes)
        {
            DateTime = dateTime;
            ShippindAddress = shippindAddress;
            BillingAddress = billingAddress;
            Notes = notes;
        }
        public decimal TotalAmount() => OrderItems.Sum(item => item.SubTotal());
    }
}
