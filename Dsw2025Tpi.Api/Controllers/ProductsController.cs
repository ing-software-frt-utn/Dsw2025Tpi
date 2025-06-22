using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
        private IProductsManagementService _productsManagmentService;

        public ProductsController(IProductsManagementService productsManagementService)
        {
           
            _productsManagmentService = productsManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel.Request request)
        {
            try
            {
                var product = await _productsManagmentService.AddProduct(request);
                return StatusCode(201,product);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (DuplicatedEntityException de)
            {
                return Conflict(de.Message);
            }
            catch (Exception)
            {
                return Problem("Se produjo un error al guardar el producto");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsManagmentService.GetProducts();

            if (!products.Any())
                return NoContent(); // 204

            return Ok(products); // 200
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productsManagmentService.GetProductById(id);

            if (product is null)
                return NotFound(); // 404

            return Ok(product); // 200
        }
    }
}
