using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Dsw2025Tpi.Domain.Entities;
namespace Dsw2025Tpi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            if (!products.Any())
                return NoContent(); // 204

            return Ok(products); // 200
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product is null)
                return NotFound(); // 404

            return Ok(product); // 200
        }
    }
}
