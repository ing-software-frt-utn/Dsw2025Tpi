using Dsw2025Ej15.Application.Dtos;
using Dsw2025Ej15.Application.Exceptions;
using Dsw2025Ej15.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductsManagementService _service;
    public ProductsController(ProductsManagementService productsManagementService)
    {
        _service = productsManagementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _service.GetProducts();
        if (products == null || !products.Any())
        {
            return NoContent();
        }
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _service.GetProductById(id);
        if (product == null)
        {
            return NotFound($"No se encontró el producto con Id {id}");
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductModel.Request request)
    {
        try
        {
            var product = await _service.AddProduct(request);
            return Created("/product", product);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (DuplicatedEntityException dee)
        {
            return Conflict(dee.Message);

        }
        catch (Exception ex)
        {
            return Problem("Se produjo un error al guardar el producto");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductModel.Request request)
    {
        try
        {
            var product = await _service.DeleteProduct(id);
            if (product == null)
            {
                return NotFound($"No se encontró el producto con Id {id}");
            }
            return Ok(product);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (Exception ex)
        {
            return Problem("Se produjo un error al actualizar el producto");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductById(Guid id, [FromBody] ProductModel.Request request)
    {
        try
        {
            var product = await _service.UpdateProduct(id, request);
            return Ok(product);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (Exception ex)
        {
            return Problem("Se produjo un error al actualizar el producto");
        }
    }
}
