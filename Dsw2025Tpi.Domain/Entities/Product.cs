using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Product : EntityBase
    {
        public string? Sku { get; set; }
        public string? Name { get; set; }
        public string? InternalCode { get; set; }
        public string? Description { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public Product() { }
        public Product(string sku, string? internalCode, string name, string? description, decimal currentUnitPrice, int stockQuantity)
        {
            Sku = sku;
            InternalCode = internalCode;
            Name = name;
            Description = description;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
            IsActive = true;
        }
        public bool ValidateStock(int quantity)
        {
            return StockQuantity >= quantity;
        }
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0) throw new InvalidOperationException("La cantidad debe ser mayor que cero.");
            if (!ValidateStock(quantity)) throw new InvalidOperationException("No hay suficiente stock.");
            StockQuantity -= quantity;
        }
    }
}
