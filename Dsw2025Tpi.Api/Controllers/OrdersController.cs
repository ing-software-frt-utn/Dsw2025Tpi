using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetOrders (
            [FromQuery] OrderStatus? status,
            [FromQuery] Guid? customerId)
            //[FromQuery] int pageNumber = 1,
            //[FromQuery] int pageSize = 10)
        {
            try
            {
                var list = await _orderManagementService.GetOrdersAsync(
                    status, 
                    customerId
                    //pageNumber: pageNumber,
                    //pageSize: pageSize
                );

                if (!list.Any())
                    return NoContent();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
