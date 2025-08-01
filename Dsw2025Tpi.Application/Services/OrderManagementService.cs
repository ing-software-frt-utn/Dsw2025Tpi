using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Services
{
    public class OrderManagementService
    {
        private readonly IRepository _repository;
        public OrderManagementService(IRepository repository)
        {
            _repository = repository; 
        }

        public async Task<OrderModel.OrderResponse> CreateOrderAsync(OrderModel.OrderRequest dtoRequest)
        {

            //subtotal y stock
            var items = new List<OrderItem>();
            decimal total = 0m;

            foreach (var item in dtoRequest.OrderItems)
            {
                var product = await _repository.GetById<Product>(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Producto '{item.ProductId}' no existe.");

                if (item.Quantity > product.StockQuantity)
                    throw new ArgumentException($"Stock insuficiente -> '{product.Name}'");

                var subTotal = item.Quantity * product.CurrentUnitPrice;
                total += subTotal;

                product.StockQuantity -= item.Quantity;
                await _repository.Update<Product>(product);

                //entidad OrderItem
                items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.CurrentUnitPrice,
                    SubTotal = subTotal
                });

            }

            //create Order
            var orderEntity = new Order
            {
                CustomerId = dtoRequest.CustomerId,
                Date = DateTime.UtcNow,
                ShippingAddress = dtoRequest.ShippingAddress, 
                BillingAddress = dtoRequest.BillingAddress,
                TotalAmount = total,
            };

            //add itemas to Order
            foreach(var oi in items)
                orderEntity.Items.Add(oi);

            //save Order (order + items)
            var createOrder = await _repository.Add<Order>(orderEntity);

            //Map DTO respuesta
            var responseItems = items
                .Select(item => new OrderModel.OrderItemResponse(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.SubTotal
                    ))
                .ToList();

            return new OrderModel.OrderResponse(
                createOrder.Id,
                createOrder.CustomerId,
                createOrder.Date,
                createOrder.ShippingAddress,
                createOrder.BillingAddress,
                createOrder.TotalAmount,
                responseItems
            );
            
        }
    }
}
