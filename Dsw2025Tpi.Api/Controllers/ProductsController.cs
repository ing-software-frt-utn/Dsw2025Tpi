using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productsManagmentService.GetProductById(id);

            if (product is null)
                return NotFound(); // 404

            return Ok(product); // 200
        }

        [HttpPatch("{id:Guid}")] 
        public async Task<IActionResult> Disable(Guid id)
        {
            var success = await _productsManagmentService.DisableProductAsync(id);

            if (!success)
                return NotFound();

            return NoContent(); // 204
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductModel.Request request)
        {
            

            // 400
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Sku) ||
                string.IsNullOrWhiteSpace(request.InternalCode) ||
                string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest();
            }

            var existingProduct = await _productsManagmentService.GetProductById(id);


            if (existingProduct is null)
                return NotFound(); // 404

            // Actualizar los campos del producto existente
            existingProduct.Sku = request.Sku;
            existingProduct.InternalCode = request.InternalCode;
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Descripcion;
            existingProduct.CurrentUnitPrice = request.Price;
            existingProduct.StockCuantity = request.Stock;
           


            await _productsManagmentService.UpdateAsync(existingProduct);

            return Ok(existingProduct); // 200


        }
    }
}
