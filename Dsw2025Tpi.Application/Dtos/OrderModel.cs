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
        public record CreateOrderRequest(
    Guid CustomerId,
    string ShippingAddress,
    string BillingAddress,
    string Notes,
    List<OrderItemModel.CreateOrderItemRequest> OrderItems);
        public record CreateOrderResponse(
             Guid id,
             Guid CustomerId,
             DateTime Date,
             string ShippingAddress,
             string BillingAddress,
             string Notes,
             OrderStatus Status,
             List<OrderItemModel.CreateOrderItemResponse>? OrderItems
            );

    }

}
