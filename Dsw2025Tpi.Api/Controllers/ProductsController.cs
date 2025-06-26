using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Dsw2025Tpi.Application.Exceptions.ApplicationException;

namespace Dsw2025Tpi.Api.Controllers;
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductsManagementService _service;

    public ProductsController(ProductsManagementService service)
    {
        _service = service;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> DisableProduct(Guid productId)
    {
        try
        {
            var product = await _service.DisableProduct(productId);
            return Ok(product);
        }
        catch (EntityNotFoundException en)
        {
            
            return NotFound(en.Message);

        }
        
        
    }

}
