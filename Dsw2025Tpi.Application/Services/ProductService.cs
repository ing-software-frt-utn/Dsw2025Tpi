using Dsw2025Tpi.Data;
using Dsw2025Tpi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly Dsw2025TpiContext _context;

        public ProductService(Dsw2025TpiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(long id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id.Equals(id) && p.IsActive);
        }
    }
}
