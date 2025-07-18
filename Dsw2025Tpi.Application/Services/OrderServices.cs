﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2025Tpi.Application.Services
{
    public class OrderServices
    {

        private readonly IRepository _repository;

        public OrderServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderModel.ResponseOrder> AddOrder(OrderModel.RequestOrder request,string userName)
        {
            if (string.IsNullOrWhiteSpace(request.shippingAddress)||
                string.IsNullOrWhiteSpace(request.billingAddress)||
                !request.orderItems.Any())
            {

                throw new ArgumentException("valores incompletos");
            }

            var customer = await _repository.First<Customer>(u => u.UserName == userName);

            if (customer is null)
            {
                throw new NotFoundEntityException($"Usuario no encontrado");
            }

     

            var items = new List<OrderItem>();
            var itemsResponse = new List<OrderItemModel.ResponseOrderItem>();
            var orden = new Order(customer.Id, request.shippingAddress, request.billingAddress, request.notes);

            foreach (var item in request.orderItems) 
            {
                var producto = await _repository.GetById<Product>(item.productId);
                if (producto is null || item.quantity < 0 || producto.StockQuantity < item.quantity || !(producto.IsActive))
                {
                    throw new ArgumentException("valores incompletos o erroneos en productos"); ;
                }
                items.Add(new OrderItem(orden.Id,item.productId,producto,item.quantity,item.currentUnitPrice));
                itemsResponse.Add(new OrderItemModel.ResponseOrderItem(item.productId, item.quantity, producto.Description, item.currentUnitPrice,item.quantity*item.currentUnitPrice));
                producto.StockQuantity -= item.quantity;
                await _repository.Update(producto);
    
            }


            orden.setOrderItems(items);


            await _repository.Add(orden);
            return new OrderModel.ResponseOrder(orden.Id,orden.CustomerId,orden.Date,orden.ShippingAddress,orden.BillingAddress,orden.Notes,orden.Date,orden.TotalAmount,orden.Status,itemsResponse);

        }

      
        public async void ValidationProducts(List<OrderItemModel.RequestOrderItem> lista) {
            foreach (var item in lista)
            {
                var producto = await _repository.GetById<Product>(item.productId);
                if (producto is null|| item.quantity<0 || producto.StockQuantity < item.quantity|| !(producto.IsActive))
                {
                    throw new ArgumentException("valores incompletos o erroneos en productos"); ;
                }
            }
        }
      
        public async Task<Order?> GetProductById(Guid id) => await _repository.GetById<Order>(id);



    }
}
