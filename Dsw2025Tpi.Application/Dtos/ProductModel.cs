using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

internal record ProductModel
{
    public record Request(string sku, string name, decimal currentUnitPrice, int stockQuantity, string? internalCode, string? description);

    public record Update(string sku, string name, decimal currentUnitPrice, int stockQuantity, string? internalCode, string? description);

    public record Response(Guid Id, string sku, string name, decimal currentUnitPrice, int stockQuantity, bool isActive);
}
