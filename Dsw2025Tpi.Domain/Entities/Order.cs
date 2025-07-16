using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order:EntityBase
    {

        public Guid customerId { get; set; }

       // public Customer _customer { get; set; }

        public DateTime date { get;} 

        public string shippingAddress { get; set; }
        public string billingAddress { get; set; }
        public string notes { get; set; }

        public decimal totalAmount { get;set; }
        public OrderStatus status { get; set; }

        public  List<OrderItem>? orderItems { get; set; }

        public Order(Guid CustomerId, string ShippingAddress, string BillingAddress, string Notes)
        {
            customerId = CustomerId;
            date = DateTime.UtcNow;
            shippingAddress = ShippingAddress;
            billingAddress = BillingAddress;
            notes = Notes;
            status = OrderStatus.Pending;
        }

        public void setOrderItems(List<OrderItem> list) {
            orderItems = list;

            totalAmount = list.Sum(p=>p.subtotal);

        
        }
    }
}
