using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

internal record OrderModel
{
    public record OrderItem(Guid ProductId, string Name, string? Description, decimal UnitPrice, int Quantity);

    public record CreateRequest(Guid CustomerId, string ShippingAddress, string BillingAddress, List<OrderItem> OrderItems);

    public record Response(Guid Id, Guid CustomerId, string ShippingAddress, string BillingAddress, DateTime Date, string Status, decimal TotalAmount, List<OrderItem> Items);

}
