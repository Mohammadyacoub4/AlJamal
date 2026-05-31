namespace AlJamal.Models;

public sealed class TableTypeInfo
{
    public int TableTypeId { get; init; }
    public string TypeName { get; init; } = "";
    public string DisplayNameAr { get; init; } = "";
    public decimal HourlyRate { get; init; }
    public decimal FirstHourRate { get; init; }
    public decimal AdditionalHourRate { get; init; }
}
