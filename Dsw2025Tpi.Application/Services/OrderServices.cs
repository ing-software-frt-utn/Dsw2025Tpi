using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
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

        public async Task<OrderModel.CreateOrderResponse?> AddOrder(OrderModel.CreateOrderRequest objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.ShippingAddress)||
                string.IsNullOrWhiteSpace(objeto.BillingAddress)||
                !objeto.OrderItems.Any()||
                (await _repository.GetById<Customer>(objeto.CustomerId)) is null
                )
            {

                throw new ArgumentException("valores incompletos");
            }
            

            this.ExistsProducts(objeto.OrderItems);

            var items = new List<OrderItem>();
            var itemsResponse = new List<OrderItemModel.CreateOrderItemResponse>();
            var productosActualizados = new List<Product>();
            var orden = new Order(objeto.CustomerId, objeto.ShippingAddress, objeto.ShippingAddress, objeto.Notes);

            foreach (var item in objeto.OrderItems) 
            {
                var producto = await _repository.GetById<Product>(item.ProductId);
                if ( producto is null|| producto._stockQuantity<= item.Quantity) 
                {
                throw new ArgumentException("valores incompletos en productos"); ;
                }
                items.Add(new OrderItem(orden.Id,item.ProductId,producto,item.Quantity,item.currentUnitPrice));
                itemsResponse.Add(new OrderItemModel.CreateOrderItemResponse(item.ProductId, item.Quantity, producto._description, item.currentUnitPrice));
                producto._stockQuantity -= item.Quantity;
                await _repository.Update(producto);
    
            }


            orden.setOrderItems(items);


            await _repository.Add(orden);
            return new OrderModel.CreateOrderResponse(orden.Id,orden._customerId,orden._date,orden._shippingAddress,orden._billingAddress,orden._notes,orden._status,itemsResponse);

        }

      
        public async void ExistsProducts(List<OrderItemModel.CreateOrderItemRequest> lista) {
            foreach (var item in lista)
            {
                var producto = await _repository.GetById<Product>(item.ProductId);
                if (producto is null|| item.Quantity<0 || producto._stockQuantity < item.Quantity|| !(producto._isActive))
                {
                    throw new ArgumentException("valores incompletos o erroneos en productos"); ;
                }
            }
        }
        public async Task<Order?> GetProductById(Guid id) => await _repository.GetById<Order>(id);



    }
}
