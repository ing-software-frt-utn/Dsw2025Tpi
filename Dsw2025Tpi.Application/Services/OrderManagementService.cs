using System;
using System.Collections.Generic;
using System.Linq;
using Dsw2025Ej15.Domain.Entities;
using Dsw2025Ej15.Domain.Interfaces;
using Dsw2025Ej15.Application.Dtos;
using System;
using System.Linq.Expressions;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using static Dsw2025Tpi.Application.Dtos.OrderModel;

namespace Dsw2025Ej15.Application.Services;

public class OrderManagementService
{
    private readonly IRepository _repository;

    public OrderManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(OrderModel.CreateRequest request)
    {
        // Verificar existencia del cliente
        var customer = await _repository.GetById<Customer>(request.CustomerId);
        if (customer == null)
            throw new Exception("Cliente no encontrado.");

        // Validar productos y stock
        var productIds = request.OrderItems.Select(i => i.ProductId).ToList();
        var products = (await _repository.GetFiltered<Product>(p => productIds.Contains(p.Id)))?.ToList()
            ?? throw new Exception("Error cargando productos.");

        foreach (var item in request.OrderItems)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
                throw new Exception($"Producto no encontrado: {item.Name}");

            if (product.StockQuantity < item.Quantity)
                throw new Exception($"Stock insuficiente para '{item.Name}'. Disponible: {product.StockQuantity}");
        }

        // Crear Order y OrderItems
        var order = new Order
        {
            CustomerId = request.CustomerId,
            ShippingAddress = request.ShippingAddress,
            BillingAddress = request.BillingAddress,
            Status = OrderStatus.Pendiente,
            Date = DateTime.UtcNow,
            Items = request.OrderItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Description = i.Description,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        // Descontar stock
        foreach (var item in request.OrderItems)
        {
            var product = products.First(p => p.Id == item.ProductId);
            product.StockQuantity -= item.Quantity;
            await _repository.Update(product);
        }

        var result = await _repository.Add(order);
        return result.Id;
    }

    public async Task<OrderModel.Response?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetById<Order>(id, nameof(Order.Items));
        if (order == null) return null;

        return new OrderModel.Response(
            order.Id,
            order.CustomerId,
            order.ShippingAddress,
            order.BillingAddress,
            order.Date,
            order.Status.ToString(),
            order.TotalAmount,
            order.Items.Select(i => new OrderModel.OrderItem(
                i.ProductId, i.Name, i.Description, i.UnitPrice, i.Quantity
            )).ToList()
        );
    }

    public async Task<IEnumerable<OrderModel.Response>> GetAllAsync()
    {
        var orders = await _repository.GetAll<Order>(nameof(Order.Items)) ?? Enumerable.Empty<Order>();

        return orders.Select(order => new OrderModel.Response(
            order.Id,
            order.CustomerId,
            order.ShippingAddress,
            order.BillingAddress,
            order.Date,
            order.Status.ToString(),
            order.TotalAmount,
            order.Items.Select(i => new OrderModel.OrderItem(
                i.ProductId, i.Name, i.Description, i.UnitPrice, i.Quantity
            )).ToList()
        ));
    }

    public async Task ChangeStatusAsync(Guid orderId, string newStatus)
    {
        var order = await _repository.GetById<Order>(orderId);
        if (order == null)
            throw new Exception("Orden no encontrada.");

        if (!Enum.TryParse<OrderStatus>(newStatus, ignoreCase: true, out var parsedStatus))
            throw new Exception("Estado no válido.");

        order.Status = parsedStatus;
        await _repository.Update(order);
    }
}
