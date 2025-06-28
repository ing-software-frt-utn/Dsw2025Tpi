﻿using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

public record OrderItemModel
{
    public record OrderItemRequest
        (
        Guid ProductId,
        int Quantity,
        string Name,
        string Description,
        decimal UnitPrice
        );
    public record OrderItemResponse
        (
        Guid ProductId,
        int Quantity,
        string Name,
        string Description,
        decimal UnitPrice
        );
}
