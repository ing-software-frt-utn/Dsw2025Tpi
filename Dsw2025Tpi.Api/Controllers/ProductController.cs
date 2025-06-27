using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _service;
        public ProductController(ProductServices servicio) => _service = servicio;

        [ProducesResponseType(typeof(ProductModel.response), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> addProduct([FromBody]ProductModel.request objeto)
        {
            try
            {
                var producto = await _service.AddProduct(objeto);
                return CreatedAtAction(nameof(GetProductById), new { id = producto.id},producto);
            }
            catch (ArgumentException e) {
                return BadRequest(e.Message);
            
            }
            catch (Exception)
            {
                return Problem("se produjo un error al agregar el producto");
            }



        }
        [HttpGet]
        public async Task <IActionResult> getProducts() { 
        var productos= await _service.GetProducts();
            if(productos is null) return NoContent();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        { 
            var producto = await _service.GetProductById(id);
            if(producto is null) return NotFound();

            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]ProductModel.request productoActualizado) 
        {
            if (string.IsNullOrWhiteSpace(productoActualizado.name) ||
                 string.IsNullOrWhiteSpace(productoActualizado.sku) ||
                 string.IsNullOrWhiteSpace(productoActualizado.internalCode) ||
                   productoActualizado.currentUnitPrice < 0) return BadRequest("los datos enviados no son válidos.");
            var producto= await _service.UpdateProduct(id, productoActualizado);
            if (producto is null) return NotFound("no se encuentra un producto con el ID proporcionado.");

           return Ok(producto);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> DisableProduct(Guid id)
        {
            if(!(await _service.DisableProduct(id)))return NotFound("no se encuentra un producto con el ID proporcionado.");
            return NoContent();
        }



    }
}
