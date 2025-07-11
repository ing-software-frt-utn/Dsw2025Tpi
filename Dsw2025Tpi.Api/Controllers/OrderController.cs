using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _service;
        public OrderController(OrderServices servicio) => _service = servicio;


        [ProducesResponseType(typeof(OrderModel.CreateOrderResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel.CreateOrderRequest objeto)
        {
            var order = await _service.AddOrder(objeto);
         
            return CreatedAtAction(nameof(GetOrderById), new {id=order.id}, order);
        
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var orden = await _service.GetProductById(id);
            if (orden is null) return NotFound();

            return Ok(orden);
        }

    }
}
