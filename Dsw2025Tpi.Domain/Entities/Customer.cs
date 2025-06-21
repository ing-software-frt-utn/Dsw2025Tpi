﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Customer : EntityBase
    {

        public Customer(string email, string name, string number)
        {
            Email = email;
            Name = name;
            PhoneNumber = number;
            Id = Guid.NewGuid();
        }
        public string? Email { get; set; } 
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }


    }
}
