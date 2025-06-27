using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities;

public class Order : EntityBase
{
    public new Guid Id { get; set; }
    public DateTime date { get; set; }
    public string shippingAdress { get; set; }
    public string billingAdress { get; set; }
    public string notes { get; set; }
    public decimal totalAmount { get; set; }


    public List<Product> products { get; set; } = new List<Product>();
    public List<Order> orders { get; set; } = new List<Order>();

}
