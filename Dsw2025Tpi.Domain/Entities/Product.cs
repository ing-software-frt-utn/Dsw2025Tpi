using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    internal class Product : EntityBase
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string? InternalCode { get; set; }
        public string? Description { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public Product() { }
        public Product(string sku, string name, string? internalCode, string? description, decimal currentUnitPrice, int stockQuantity)
        {
            Sku = sku;
            Name = name;
            InternalCode = internalCode;
            Description = description;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
            IsActive = true;
        }
        public void SkuValidate()
        {
            if (Sku == this.Sku) throw new Exception();
        }
        public void StockValidate()
        {
            if (StockQuantity < 0) throw new ArgumentOutOfRangeException();
        }
        public void PriceValidate()
        {
            if(CurrentUnitPrice <=0) throw new ArgumentOutOfRangeException();
        }
    }
}
