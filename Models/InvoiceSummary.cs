namespace AlJamal.Models;

public sealed class InvoiceSummary
{
    public int InvoiceId { get; init; }
    public int PlayerSessionId { get; init; }
    public string? PlayerName { get; init; }
    public string TableName { get; init; } = "";
    public int PlayMinutes { get; init; }
    public decimal PlayAmount { get; init; }
    public decimal OrdersAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? PrintedAt { get; init; }
}
