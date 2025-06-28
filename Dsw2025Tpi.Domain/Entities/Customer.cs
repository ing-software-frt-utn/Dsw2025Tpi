using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer:EntityBase
    {
        public string _name { get; set; }
        public string _email { get; set; }
        public string _phoneNumber { get; set; }



        public Customer(string name, string email, string phoneNumber) 
        {
            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
        } 
    }
}
