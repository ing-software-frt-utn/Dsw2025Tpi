using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order : EntityBase
    {
        public OrderStatus Status { get; set; } 
        public DateTime CreateDate { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string Notes { get; set; }
        public decimal TotalAmount { get; set; }

        public Order(DateTime createDate, string shippingAddress, string billingAddress, string notes, decimal totalAmount)
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Pending; // Default status
            CreateDate = createDate;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Notes = notes;
            TotalAmount = totalAmount;
        }
    }
}
