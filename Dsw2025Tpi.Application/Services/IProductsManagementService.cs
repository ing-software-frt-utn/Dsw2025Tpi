using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Services
{
    public interface IProductsManagementService
    {
        Task<ProductModel.Response> AddProduct(ProductModel.Request request);
        Task<Product?> GetProductById(Guid id);
        Task<List<Product>?> GetProducts();
        Task<Product> UpdateAsync(Product product);
    }
}