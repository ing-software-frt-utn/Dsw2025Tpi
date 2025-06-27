using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Product:EntityBase
    {

        public Product(string sku, string internalCode, string name, string description,
                 decimal currentUnitPrice, int stockQuantity)
        {
            _sku = sku;
            _internalCode = internalCode;
            _name = name;
            _description = description;
            _currentUnitPrice = currentUnitPrice;
            _stockQuantity = stockQuantity;
            _isActive = true;
        }

        public string _sku { get; set; }
        public string _internalCode { get; set; }

        public string _name { get; set; }

        public string _description { get; set; }

        public decimal _currentUnitPrice { get; set; }

        public int _stockQuantity { get; set; }

        public bool _isActive { get; set; }


    }
}
