﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Order:EntityBase
    {

        public Guid _customerId { get; set; }

       // public Customer _customer { get; set; }

        public DateTime _date { get;} 

        public string _shippingAddress { get; set; }
        public string _billingAddress { get; set; }
        public string _notes { get; set; }

        public decimal _totalAmount { get;set; }
        public OrderStatus _status { get; set; }

        public List<OrderItem> _orderItems { get; set; }

        public Order(Guid customerId, string shippingAddress, string billingAddress, string notes)
        {
            _customerId = customerId;
            //_customer = customer;
            _date = DateTime.UtcNow;
            _shippingAddress = shippingAddress;
            _billingAddress = billingAddress;
            _notes = notes;
            _status = OrderStatus.Pending;
        }

        public void setOrderItems(List<OrderItem> list) {
            _orderItems = list;

            _totalAmount = list.Sum(p=>p._subtotal);

        
        }
    }
}
