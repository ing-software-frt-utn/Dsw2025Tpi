using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    internal class Customer : EntityBase
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public Customer(string? email, string? name, string? phoneNumber)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
