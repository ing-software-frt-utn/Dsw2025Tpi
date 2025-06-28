using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos
{
    public class ProductModel
    {
        public record Request(
            string Sku,
            string InternalCode,
            string Name,
            string Description,
            decimal CurrentUnitPrice,
            int StockQuantity
        );

        public record Response (
            Guid Id,
            string Sku,
            string InternalCode,
            string Name,
            string Description,
            decimal CurrentUnitPrice,
            int StockQuantity
        );
    }
}
