﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos
{
    public record ProductModel
    {
        public record request(string sku, string internalCode, string name, string description,
                 decimal currentUnitPrice, int stockQuantity);

        public record response(Guid id);

    }
}
