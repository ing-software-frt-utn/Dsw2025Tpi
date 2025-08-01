using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos
{
    public class OrderModel
    {
        public record OrderRequest(
            Guid CustomerId,
            string ShippingAddress,
            string BillingAddress,
            List<OrderItemRequest> OrderItems
        );

        public record OrderResponse(
            Guid OrderId,
            Guid CustomerId,
            DateTime Date,
            string ShippingAddress,
            string BillingAddres,
            decimal TotalAmount,
            List<OrderItemResponse> Items
            );

        public record OrderItemRequest(
            Guid ProductId,
            int Quantity,
            string Name,
            string Description,
            decimal CurrentUnitPrice
        );

        public record OrderItemResponse(
            Guid ProductId,
            int Quantity,
            decimal UnitPrice,
            decimal SubTotal
            );


    }
}
