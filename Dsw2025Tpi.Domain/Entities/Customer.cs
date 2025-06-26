using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public Customer() { }
        public Customer(string email, string name, string phoneNumber, List<Order> orders, Guid orderId)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Orders = orders;
            OrderId = orderId;
        }
        public Guid OrderId { get; }
    }
}
