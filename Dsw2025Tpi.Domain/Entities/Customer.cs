using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer:EntityBase
    {
        public required string name { get; set; }
        public required string email { get; set; }
        public required string phoneNumber { get; set; }



        public Customer(string Name, string Email, string PhoneNumber) 
        {
            name = Name;
            email = Email;
            phoneNumber = PhoneNumber;
        } 
    }
}
