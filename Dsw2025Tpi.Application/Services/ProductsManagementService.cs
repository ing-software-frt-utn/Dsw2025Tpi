using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Interfaces;
using static Dsw2025Tpi.Application.Dtos.ProductModel;
using Microsoft.EntityFrameworkCore;



namespace Dsw2025Tpi.Application.Services;

public class ProductsManagementService : IProductsManagementService
{
    private readonly IRepository _repository;

    public ProductsManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await _repository.GetById<Product>(id);
    }


    public async Task<List<Product>?> GetProducts()
    {
        var products = await _repository.GetAll<Product>();
        return products?.ToList() ?? new List<Product>();
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku) ||
            string.IsNullOrWhiteSpace(request.InternalCode) ||
            string.IsNullOrWhiteSpace(request.Descripcion) ||
            string.IsNullOrWhiteSpace(request.Name) ||
            request.Price < 0 ||
            request.Stock < 0)
        {
            throw new ArgumentException("Valores para el producto no validos");
        }

        var exist = await _repository.First<Product>(p => p.InternalCode == request.InternalCode);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.InternalCode}");

        var product = new Product(request.Sku, request.InternalCode, request.Descripcion, request.Name, request.Price, request.Stock);

        await _repository.Add(product);
        return new ProductModel.Response(product.Id);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var existingProduct = await GetProductById(product.Id);

        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.CurrentUnitPrice = product.CurrentUnitPrice;
        existingProduct.StockCuantity = product.StockCuantity;
        existingProduct.Sku = product.Sku;
        existingProduct.InternalCode = product.InternalCode;
        existingProduct.IsActive = product.IsActive;

        await _repository.Update(existingProduct);

        return existingProduct;


    }




}


