using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductServices
    {
        private readonly IRepository _repository;

        public ProductServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>?> GetProducts()=> (await _repository.GetAll<Product>())?.ToList();


        public async Task<ProductModel.response> AddProduct(ProductModel.request objeto) {

            if (string.IsNullOrWhiteSpace(objeto.name) ||
                  string.IsNullOrWhiteSpace(objeto.sku) ||
                  string.IsNullOrWhiteSpace(objeto.internalCode) ||
                    objeto.currentUnitPrice < 0)
            {
                throw new ArgumentException("valores incompletos");
            }
            var existenciaSku = await _repository.First<Product>(P => P._sku == objeto.sku);
            if (existenciaSku is not null) throw new ArgumentException("ya existe un producto con ese sku");

            var existenciaIC = await _repository.First<Product>(P => P._internalCode == objeto.internalCode);
            if (existenciaIC is not null) throw new ArgumentException("ya existe un producto con ese interalCode");


            var producto = new Product(objeto.sku, objeto.internalCode, objeto.name, objeto.description, objeto.currentUnitPrice, objeto.stockQuantity);


                await _repository.Add(producto);
            return new ProductModel.response(producto.Id);

        }

        public async Task<Product?> GetProductById(Guid id)=> await _repository.GetById<Product>(id);

        public async Task<Product?> UpdateProduct(Guid id,ProductModel.request productoActualizado) {
            var producto = await _repository.GetById<Product>(id);
            
            if (producto is null) return null;

            producto._sku= productoActualizado.sku;
            producto._internalCode = productoActualizado.internalCode;
            producto._name = productoActualizado.name;
            producto._description = productoActualizado.description;
            producto._currentUnitPrice = productoActualizado.currentUnitPrice;
            producto._stockQuantity = productoActualizado.stockQuantity;

            await _repository.Update(producto);

            return producto;
        }

        public async Task<Boolean> DisableProduct(Guid id)
        {
            var producto = await _repository.GetById<Product>(id);
            if (producto is null) return false;
            producto._isActive = false;
            await _repository.Update(producto);
            return true;
        }



    }
}
