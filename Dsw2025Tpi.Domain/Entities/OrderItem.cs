using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities;

public class OrderItem : EntityBase
{
    public OrderItem()
    {
    }

    public new Guid Id { get; set; }
    public int quantity { get; set; }
    public decimal unitPrice { get; set; }
    // Navigation properties
    public Order Order { get; set; }
    public Product Product { get; set; }
}
