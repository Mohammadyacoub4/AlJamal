namespace AlJamal.Models;

public sealed class OrderItemInfo
{
    public int OrderItemId { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; } = "";
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal LineTotal => Quantity * UnitPrice;
    public DateTime AddedAt { get; init; }
}
