using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string? EMail { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Order> Orders { get;} = new HashSet<Order>();

        public Customer()
        {
            
        }
        public Customer(string email, string name, string phoneNumber)
        {
            EMail = email;
            Name = name;    
            PhoneNumber = phoneNumber;
        }
    }
}
