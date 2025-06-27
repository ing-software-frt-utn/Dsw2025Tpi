using Dsw2025Ej15.Application.Dtos;
using Dsw2025Ej15.Application.Exceptions;
using Dsw2025Tpi.Domain.Interfaces;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Ej15.Application.Services;

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
            new ProductModel.Response(product.Id, product.Sku!, product.InternalCode!,
            product.Name!, product.Description!, product.CurrentUnitPrice,
            product.StockQuantity, product.IsActive) :
            null;
    }
    public async Task<IEnumerable<ProductModel.Response>?> GetProducts()
    {
        var products = await _repository.GetFiltered<Product>(p => p.IsActive);
        return products?.Select(p => new ProductModel.Response(p.Id, p.Sku!, p.InternalCode!,
                p.Name!, p.Description!, p.CurrentUnitPrice, p.StockQuantity, p.IsActive));
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku) ||
            string.IsNullOrWhiteSpace(request.Name) ||
            request.CurrentUnitPrice <= 0 || request.StockQuantity <0)
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }

        var exist = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        var product = new Product(request.Sku, request.InternalCode, request.Name,
            request.Description, request.CurrentUnitPrice, request.StockQuantity);
        await _repository.Add(product);
        return new ProductModel.Response(product.Id, product.Sku!, product.InternalCode!,
            product.Name!, product.Description!, product.CurrentUnitPrice,
            product.StockQuantity, product.IsActive);
    }
    public async Task<ProductModel.Response> DeleteProduct(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        if (product == null) throw new EntityNotFoundException($"El producto con Id {id} no existe");
        product.IsActive = false;
        await _repository.Update(product);
        return new ProductModel.Response(product.Id, product.Sku!, product.InternalCode!,
            product.Name!, product.Description!, product.CurrentUnitPrice,
            product.StockQuantity, product.IsActive);
    }
}
