namespace AlJamal.Models;

public sealed class InvoiceDraft
{
    public int PlayerSessionId { get; init; }
    public string PlayerLabel { get; init; } = "";
    public string TableName { get; init; } = "";
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int PlayMinutes { get; init; }
    public decimal PlayHours { get; init; }
    public decimal HourlyRate { get; init; }
    public decimal FirstHourRate { get; init; }
    public decimal AdditionalHourRate { get; init; }
    public decimal PlayAmount { get; init; }
    public decimal OrdersAmount { get; init; }
    public decimal TotalAmount => PlayAmount + OrdersAmount;
    public List<InvoiceLineDraft> Lines { get; init; } = [];
}

public sealed class InvoiceLineDraft
{
    public string LineType { get; init; } = "";
    public string Description { get; init; } = "";
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal LineTotal { get; init; }
}
