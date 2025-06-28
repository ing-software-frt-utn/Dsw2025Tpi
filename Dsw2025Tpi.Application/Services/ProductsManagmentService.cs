using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductsManagmentService
    {
        private readonly IRepository _repository;

        public ProductsManagmentService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductModel.Response> CreateProductAsync (ProductModel.Request dto)
        {
            if(string.IsNullOrEmpty(dto.Sku) ||
               string.IsNullOrWhiteSpace(dto.Name) ||
               dto.CurrentUnitPrice <= 0 ||
               dto.StockQuantity < 0 )
            {
                throw new ArgumentException("Datos inválidos para crear el producto.");
            }

            var existe = await _repository.First<Product>(p => p.Sku == dto.Sku);
            if (existe != null)
                throw new DuplicatedEntityException($"SKU '{dto.Sku}' ya existe.");

            var product = new Product (
                dto.Sku,
                dto.InternalCode,
                dto.Name,
                dto.Description,
                dto.CurrentUnitPrice,
                dto.StockQuantity
            );

            var created = await _repository.Add<Product>(product);

            return new ProductModel.Response(
                created.Id,
                created.Sku,
                created.InternalCode,
                created.Name,
                created.Description,
                created.CurrentUnitPrice,
                created.StockQuantity
            );
           
        }

        public async Task<List<ProductModel.Response>> GetProductsAsync()
        {
            var products = await _repository.GetAll<Product>();

            if (products == null || !products.Any())
                return new List<ProductModel.Response>();

            var productsList = products
                .Select(p => new ProductModel.Response(
                    p.Id,
                    p.Sku,
                    p.InternalCode,
                    p.Name,
                    p.Description,
                    p.CurrentUnitPrice,
                    p.StockQuantity))
                .ToList();

            return productsList;
        }

        public async Task<ProductModel.Response> GetProductById(Guid id)
        {
            var product = await _repository.GetById<Product>(id);

            if (product == null)
                return null;

            return new ProductModel.Response(
                product.Id,
                product.Sku,
                product.InternalCode,
                product.Name,
                product.Description,
                product.CurrentUnitPrice,
                product.StockQuantity
                );
        }

    }
}
