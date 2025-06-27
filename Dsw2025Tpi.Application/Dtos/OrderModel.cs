using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderModel
    {
        public record OrderRequest
            (
            Guid CustomerId,
            string? ShippingAddress,
            string? BillingAddress,
            List<OrderItem> OrderItems
            );
        public record OrderResponse
            (
            Guid OrderId,
            Guid CustomerId,
            string? ShippingAddress,
            string? BillingAddress,
            List<OrderItem> OrderItems,
            decimal TotalAmount
            );
    }
}
