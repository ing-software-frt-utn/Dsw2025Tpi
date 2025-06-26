using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services;

public class ProductsManagementService
{
    private readonly IRepository _repository;

    public ProductsManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductModel.Response?> GetProductById(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        return product != null ?
            new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice) :
            null;
    }

    public async Task<IEnumerable<ProductModel.Response>?> GetProducts()
    {
        return (await _repository
            .GetFiltered<Product>(p => p.IsActive))?
            .Select(p => new ProductModel.Response(p.Id, p.Sku, p.Name, 
            p.CurrentUnitPrice));
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku) || 
            string.IsNullOrWhiteSpace(request.Name) ||
            request.Price < 0)
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }

        var exist = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        var product = new Product(request.Sku, request.Name, request.Price);
        await _repository.Add(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name,
            product.CurrentUnitPrice);
    }

    //Preguntar al profesor si esta funcion esta hecha correctamente; me genera inquietud si deberia hacerlo asi o deberia de usar la funcion GetProductById 
    public async Task<ProductModel.Response?> DisableProduct(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        if (product == null) throw new EntityNotFoundException("No se encontro el producto que se desea obtener");
        product.IsActive = false;
        product = await _repository.Update(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice);
    }
}
