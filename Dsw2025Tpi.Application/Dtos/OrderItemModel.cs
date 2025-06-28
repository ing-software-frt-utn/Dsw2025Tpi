using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;

namespace Dsw2025Tpi.Application.Dtos
{
    public record OrderItemModel
    {
        public record CreateOrderItemRequest(
              Guid ProductId,
              int Quantity,
              decimal currentUnitPrice);
        public record CreateOrderItemResponse(
     Guid ProductId,
          int Quantity,
          string description,
          decimal currentUnitPrice

    );
    }

             
        
    
    
      
}
