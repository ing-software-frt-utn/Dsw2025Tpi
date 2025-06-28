using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsManagmentService _service;

        public ProductsController(ProductsManagmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel.Request dto)
        {
            try
            {
                var pruduct = await _service.CreateProductAsync(dto);
                return StatusCode(StatusCodes.Status201Created, pruduct);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message); //400
            }
            catch (DuplicatedEntityException de)
            {
                return Conflict(de.Message); //409
            }
            catch (Exception)
            {
                return Problem("Se produjo un error al guardar el producto.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            if (products.Count == 0)
                return NoContent(); //204

            return Ok(products); //200
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _service.GetProductById(id);

            if(product == null)
                return NotFound(); //404
            return Ok(product);
        }
    }
}
