﻿using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _service;
        public OrderController(OrderServices servicio) => _service = servicio;


        [ProducesResponseType(typeof(OrderModel.ResponseOrder), StatusCodes.Status201Created)]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel.RequestOrder objeto)
        {

            var user= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var order = await _service.AddOrder(objeto,user);
         
            return CreatedAtAction(nameof(GetOrderById), new {id=order.id}, order);
    
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var orden = await _service.GetProductById(id);
            if (orden is null) return NotFound();

            return Ok(orden);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetOrders(
        OrderStatus? status,
        Guid? customerId)

        {
            var Orders = await _service.GetFilteredOrders(status, customerId);
            if (!Orders.Any()) return NoContent();

            return Ok(Orders);
        }

    }
}
