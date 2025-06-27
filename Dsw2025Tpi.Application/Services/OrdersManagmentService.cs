using Dsw2025Ej15.Application.Dtos;
using Dsw2025Ej15.Application.Exceptions;
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
    public class OrdersManagmentService
    {
        private readonly IRepository _repository;
        public OrdersManagmentService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<OrderModel.OrderResponse> AddOrder(OrderModel.OrderRequest request)
        {
            var exist = await _repository.GetById<Customer>(request.CustomerId);
            if (exist == null) throw new EntityNotFoundException($"El cliente con Id {request.CustomerId} no existe");

            if (request.OrderItems == null || string.IsNullOrEmpty(request.ShippingAddress)
               || string.IsNullOrEmpty(request.BillingAddress))
            {
                throw new ArgumentException("Valores para el pedido no válidos");
            }

            foreach (var item in request.OrderItems)
            {
                var product = await _repository.GetById<Product>(item.ProductId);
                if (product == null) throw new EntityNotFoundException($"No se encontró el producto con ID {item.ProductId}");
                product.ReduceStock(item.Quantity);
                await _repository.Update(product);
            }

            var orderItems = new List<OrderItem>();
            orderItems = [];
            var order = new Order(request.CustomerId, request.ShippingAddress, request.BillingAddress, orderItems);

            foreach (var item in request.OrderItems)
            {
                var orderItem = new OrderItem(item.ProductId, item.Quantity, item.Product!.Name!, item.Product.Description!, item.UnitPrice, order.Id);
                orderItems.Add(orderItem);
                await _repository.Add(orderItem);
            }

            await _repository.Add(order);
            return new OrderModel.OrderResponse(
                order.Id,
                order.CustomerId,
                order.ShippingAddress,
                order.BillingAddress,
                orderItems,
                order.TotalAmount
            );
        }
    }
}
