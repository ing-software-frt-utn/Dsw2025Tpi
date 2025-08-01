using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderManagementService _orderManagementService;
        public OrdersController(OrderManagementService orderManagementService)
        {
            _orderManagementService = orderManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel.OrderRequest orderDto)
        {
            if (orderDto == null)
                return BadRequest(new { error = "El cuerpo de la petición no puede estar vacio." });

            if (orderDto.OrderItems == null || !orderDto.OrderItems.Any())
                return BadRequest(new {error = "La orden debe contener al menos un item."});

            try
            {
                var order = await _orderManagementService.CreateOrderAsync(orderDto);

                return StatusCode(StatusCodes.Status201Created, order);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(new {error = ae.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = "Error interno al crear la orden."}); //middelware 
            }
        }
    }
}
