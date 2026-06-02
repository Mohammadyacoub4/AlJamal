namespace AlJamal.Models;

public sealed class TableInvoiceDraft
{
    public int TableId { get; init; }
    public string TableName { get; init; } = "";
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    
    // تفاصيل وقت اللعب للطاولة ككل
    public int PlayMinutes { get; set; }
    public decimal PlayHours { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal FirstHourRate { get; set; }
    public decimal AdditionalHourRate { get; set; }
    public decimal PlayAmount { get; set; }

    public decimal TotalAmount { get; set; }
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
    public decimal FirstHourRate { get; init; }
    public decimal AdditionalHourRate { get; init; }
    public decimal PlayAmount { get; set; }
    public List<OrderItemInfo> Orders { get; init; } = [];
    public decimal OrdersAmount { get; init; }
    public decimal PlayerTotal => PlayAmount + OrdersAmount;
}
