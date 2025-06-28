using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductServices
    {
        private readonly IRepository _repository;

        public ProductServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>?> GetProducts() {
          var product =  await _repository.GetAll<Product>(); 
        if(product is null) throw new NotFoundEntityException("no se encontraron productos");
            return product.ToList();
        }


        public async Task<Product?> AddProduct(ProductModel.request objeto) {

            if (objeto.currentUnitPrice <= 0)
            {
                throw new NullPriceException("Precio menor o igual a cero");
            }

            if (string.IsNullOrWhiteSpace(objeto.name) ||
                  string.IsNullOrWhiteSpace(objeto.sku) ||
                  string.IsNullOrWhiteSpace(objeto.internalCode)
                    )
            {
                throw new ArgumentException("valores incompletos");
            }
            var existenciaSku = await _repository.First<Product>(P => P._sku == objeto.sku);
            if (existenciaSku is not null) throw new DuplicatedEntityException("ya existe un producto con ese sku");

            var existenciaIC = await _repository.First<Product>(P => P._internalCode == objeto.internalCode);
            if (existenciaIC is not null) throw new DuplicatedEntityException("ya existe un producto con ese internalCode");


            var producto = new Product(objeto.sku, objeto.internalCode, objeto.name, objeto.description, objeto.currentUnitPrice, objeto.stockQuantity);


                await _repository.Add(producto);
            return producto;

        }

        public async Task<Product?> GetProductById(Guid id) {
           var product= await _repository.GetById<Product>(id);
            if (product is null) throw new NotFoundEntityException("no se econtro el producto");
            return product;
        } 

        public async Task<Product?> UpdateProduct(Guid id,ProductModel.request productoActualizado) {
            if (productoActualizado.currentUnitPrice <= 0)
            {
                throw new NullPriceException("Precio menor o igual a cero");
            }


            if (string.IsNullOrWhiteSpace(productoActualizado.name) ||
               string.IsNullOrWhiteSpace(productoActualizado.sku) ||
               string.IsNullOrWhiteSpace(productoActualizado.internalCode) 
                ) throw new ArgumentException("valores incompletos");
           
            
            var producto = await _repository.GetById<Product>(id);
            


            if (producto is null) throw new NotFoundEntityException("no se econtro el producto");

            producto._sku= productoActualizado.sku;
            producto._internalCode = productoActualizado.internalCode;
            producto._name = productoActualizado.name;
            producto._description = productoActualizado.description;
            producto._currentUnitPrice = productoActualizado.currentUnitPrice;
            producto._stockQuantity = productoActualizado.stockQuantity;

            await _repository.Update(producto);

            return producto;
        }

        public async void DisableProduct(Guid id)
        {
            var producto = await _repository.GetById<Product>(id);
            if (producto is null) throw new NotFoundEntityException("no se econtro el producto");
            producto._isActive = false;
            await _repository.Update(producto);
        }



    }
}
