using Dsw2025Ej15.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IProductsManagementService
    {
        public Task<ProductModel.Response?> GetProductById(Guid id);
        public Task<IEnumerable<ProductModel.Response>?> GetProducts();
        public Task<ProductModel.Response> AddProduct(ProductModel.Request request);
        public Task<ProductModel.Response> DeleteProduct(Guid id);
        public Task<ProductModel.Response> UpdateProduct(Guid id, ProductModel.Request request);
    }
}
