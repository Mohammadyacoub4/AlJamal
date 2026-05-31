namespace AlJamal.Models;

public sealed class TableInvoiceDraft
{
    public int TableId { get; init; }
    public string TableName { get; init; } = "";
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public decimal TotalAmount { get; init; }
    public List<PlayerInvoiceDetail> Players { get; init; } = [];
}

public sealed class PlayerInvoiceDetail
{
    public int PlayerSessionId { get; init; }
    public string PlayerLabel { get; init; } = "";
    public DateTime PlayerStartTime { get; init; }
    public DateTime PlayerEndTime { get; init; }
    public int PlayMinutes { get; init; }
    public decimal PlayHours { get; init; }
    public decimal HourlyRate { get; init; }
    public decimal PlayAmount { get; init; }
    public List<OrderItemInfo> Orders { get; init; } = [];
    public decimal OrdersAmount { get; init; }
    public decimal PlayerTotal => PlayAmount + OrdersAmount;
}
