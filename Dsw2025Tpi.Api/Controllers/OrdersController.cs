using Dsw2025Ej15.Application.Exceptions;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/orders/")]
public class OrdersController : ControllerBase
{
    private readonly OrdersManagmentService _service;
    public OrdersController(OrdersManagmentService ordersManagmentService)
    {
        _service = ordersManagmentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderModel.OrderRequest request)
    {
        try
        {
            var order = await _service.AddOrder(request);
            return Created("api/order", order);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (InvalidOperationException io)
        {
            return BadRequest(io.Message);
        }
        catch (EntityNotFoundException enfe)
        {
            return BadRequest(enfe.Message);
        }
        catch (DuplicatedEntityException dee)
        {
            return BadRequest(dee.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
