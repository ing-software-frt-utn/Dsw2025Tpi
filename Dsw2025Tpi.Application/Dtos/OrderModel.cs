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
        public record RequestOrder(
    Guid customerId,
    string shippingAddress,
    string billingAddress,
    string notes,
    List<OrderItemModel.RequestOrderItem> orderItems);
        public record ResponseOrder(
             Guid id,
             Guid customerId,
             DateTime date,
             string shippingAddress,
             string billingAddress,
             string notes,
             DateTime dateOrder,
             decimal totalAmount,
             OrderStatus status,
             List<OrderItemModel.ResponseOrderItem>? orderItems
            );

    }

}
