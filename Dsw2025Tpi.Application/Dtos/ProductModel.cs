namespace Dsw2025Ej15.Application.Dtos;

public record ProductModel
{
    public record ProductRequest
        (
        string Sku,
        string InternalCode,
        string Name,
        string Description,
        decimal UnitPrice,
        int Quantity
        );

    public record ProductResponse
        (
        Guid ProductId,
        string Sku,
        string InternalCode,
        string Name,
        string Description,
        decimal UnitPrice,
        int Quantity,
        bool IsActive
        );
}
