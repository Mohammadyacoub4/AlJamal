namespace AlJamal.Models;

public sealed class ProductInfo
{
    public int ProductId { get; init; }
    public string ProductName { get; init; } = "";
    public string? Category { get; init; }
    public decimal UnitPrice { get; init; }
    public bool IsActive { get; init; } = true;

    public string Display => $"{ProductName} — {UnitPrice:N2} د.أ";
}
